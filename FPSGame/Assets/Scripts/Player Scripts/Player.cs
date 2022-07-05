using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100.0f;
    public WeaponHandBehavior weaponHand;
    public GameObject InteractableObject;

    #region Camera
    public Camera cam;
    public Light eyeSight;
    public Transform camTransform;
    
    public bool lockCamera = false;
    public bool lockMovement = false;

    [Range(60.0f, 100.0f)]
    public float fov = 60.0f;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 70.0f;
    public float eyeSightRange = 7.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private readonly float checkObjectRange = 3.0f;
    #endregion

    #region Movement
    public bool crouched;
    public bool runActive;
    private bool isSupposedToCrouch = false;
    private bool wantsToRun = false;

    public float walkSpeed = 4.0f;
    public float runSpeed = 6.0f;
    public float jumpHeight = 5.0f;
    public float stamina = 100.0f;
    public float staminaDrain = 14.0f;
    public float staminaGain = 7.0f;
    public float crouchModifier = .5f;

    private float verticalVelocity;
    private const float gravity = -15.0f;
    private const float terminalVelocity = -15.0f;

    private bool inAir = true;
    private bool canStand = true;
    #endregion

    private PlayerEventController pec;
    private CharacterController cc;

    #region Monobehavior
    private void Awake()
    {
        pec = GetComponent<PlayerEventController>();
        cc = GetComponent<CharacterController>();

        cam.fieldOfView = fov;
        camTransform = cam.transform;

        GameMasterBehavior.GameMaster.playerObject = gameObject;
        GameMasterBehavior.GameMaster.playerController = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        eyeSight.range = eyeSightRange;

        GUIScript.instance.TriggerHealth(health, CheckIfDead());
    }

    private void OnEnable()
    {
        pec.OnPressInteract += InteractObject;
        pec.OnPressJump += Jump;
        pec.OnPressCrouch += ToggleCrouch;
        pec.OnPressRun += ToggleRun;
    }

    private void OnDisable()
    {
        pec.OnPressInteract -= InteractObject;
        pec.OnPressJump -= Jump;
        pec.OnPressCrouch -= ToggleCrouch;
        pec.OnPressRun -= ToggleRun;
    }

    private void Update()
    {
        inAir = !cc.isGrounded;

        CheckAboveHead();

        if (!lockCamera)
        {
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.eulerAngles = new Vector3(0, yaw, 0);
            cam.transform.localEulerAngles = new Vector3(pitch, 0, 0);

            CheckForObject();
        }
        if (!lockMovement)
        {
            Run(wantsToRun);

            Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (inAir)
            {
                verticalVelocity += gravity * Time.deltaTime;
                verticalVelocity = Mathf.Clamp(verticalVelocity, terminalVelocity, -terminalVelocity);
            }
            cc.Move(calcMovement(movement) * Time.deltaTime);
            cc.Move(new Vector3(0, verticalVelocity * Time.deltaTime, 0));

            if(crouched != isSupposedToCrouch)
            {
                Crouch();
            }
        }

        Debug.DrawRay(transform.position, new Vector3(transform.forward.x * transform.forward.x,
            transform.forward.y * transform.forward.y,
            transform.forward.z * transform.forward.z) * 10, Color.yellow);
    }

    private void FixedUpdate()
    {

        
    }
    #endregion

    #region controls
    private void Jump()
    {
        if (!inAir)
        {
            verticalVelocity = jumpHeight;
        }
    }

    private void ToggleCrouch()
    {
        isSupposedToCrouch = !isSupposedToCrouch;
    }

    private void ToggleRun()
    {
        
        wantsToRun = !wantsToRun;
    }

    private void Crouch()
    {
        if (!crouched)
        {
            cc.center = new Vector3(0, .625f, 0);
            cc.height = 1.25f;
            cam.transform.localPosition = new Vector3(0, 1, 0);
            crouched = true;
        }
        else
        {
            if (canStand)
            {
                cc.center = new Vector3(0, 1.25f, 0);
                cc.height = 2.5f;
                cam.transform.localPosition = new Vector3(0, 2, 0);
                crouched = false;
            }
            
        }
    }

    private void Run(bool trigger)
    {
        if (trigger)
        {
            if(stamina >= 10.0f)
            {
                runActive = true;
            }
        }
        else
        {
            runActive = false;
        }
    }

    private void InteractObject()
    {
        if(InteractableObject != null)
        {
            if (InteractableObject.GetComponent<InteractableWeapon>() != null)
            {
                InteractableObject.GetComponent<InteractableWeapon>().PickUpWeapon();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        health = Mathf.Clamp(health, 0.0f, 100.0f);

        GUIScript.instance.TriggerHealth(health, CheckIfDead());
    }

    private bool CheckIfDead()
    {
        return (health < 1.0) ? true : false;
    }

    private Vector3 calcMovement(Vector3 inputs)
    {
        Vector3 output = transform.TransformDirection(inputs.normalized);
        CalculateStamina(inputs.x, inputs.z);

        if(runActive && stamina > 0)
        {
            output = output * runSpeed;
        }
        else
        {
            output = output * walkSpeed;
        }

        output = (crouched) ? output * crouchModifier : output;

        return output;
    }
    #endregion

    #region raycast checks
    private void CheckForObject()
    {
        InteractableObject = null;

        RaycastHit checker;

        Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * checkObjectRange, Color.yellow);
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out checker, checkObjectRange, GameMasterBehavior.ObjectLayer))
        {
            InteractableObject = checker.collider.gameObject;
        }
    }
    
    //checks above head to see if you should be able to uncrouch
    private void CheckAboveHead()
    {
        float distance = 1.5f;

        Debug.DrawRay(cam.transform.position, Vector3.up * distance, Color.green);
        canStand = Physics.Raycast(cam.transform.position, Vector3.up, out RaycastHit hit, distance) ? false : true;
    }
    #endregion

    private void CalculateStamina(float x, float z)
    {
        if (runActive)
        {
            if(z != 0.0f || x != 0.0f)
            {
                stamina -= staminaDrain * Time.deltaTime;
            }
            else
            {
                stamina += staminaGain * Time.deltaTime;
            }
        }
        else
        {
            if (z != 0.0f || x != 0.0f)
            {
                stamina += (staminaGain * .5f) * Time.deltaTime;
            }
            else
            {
                stamina += staminaGain * Time.deltaTime;
            }
        }

        stamina = Mathf.Clamp(stamina, 0.0f, 100.0f);

        if(stamina == 0.0f)
        {
            runActive = false;
        }
    }
}
