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

    public float walkSpeed = 1.0f;
    public float jumpHeight = 5.0f;

    private bool inAir = true;
    #endregion

    private Rigidbody rb;
    private PlayerEventController pec;

    #region Monobehavior
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pec = GetComponent<PlayerEventController>();

        cam.fieldOfView = fov;
        camTransform = cam.transform;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameMasterBehavior.GameMaster.playerObject = gameObject;
        GameMasterBehavior.GameMaster.playerController = this;
        eyeSight.range = eyeSightRange;
    }

    private void OnEnable()
    {
        pec.OnPressInteract += InteractObject;
        pec.OnPressJump += Jump;
        pec.OnPressCrouch += Crouch;
    }

    private void OnDisable()
    {
        pec.OnPressInteract -= InteractObject;
        pec.OnPressJump -= Jump;
        pec.OnPressCrouch -= Crouch;
    }

    private void Update()
    {
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
            CheckForGround();
        }
    }

    private void FixedUpdate()
    {
        if (!lockMovement)
        {
            Vector3 targetVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity.normalized) * walkSpeed;

            Vector3 velocityChange = targetVelocity - rb.velocity;
            velocityChange.y = 0;
            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
        
    }
    #endregion

    #region controls
    private void Jump()
    {
        if (!inAir)
        {
            rb.AddForce(0f, jumpHeight, 0f, ForceMode.Impulse);
            inAir = true;
        }
    }

    private void Crouch()
    {
        if (!crouched)
        {
            Debug.Log("Crouch");
            crouched = true;
        }
        else
        {
            Debug.Log("Stand");
            crouched = false;
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
    #endregion

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

    private void CheckForGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + (transform.localScale.y * .1f), transform.position.z);
        float distance = .75f;
        Debug.DrawRay(origin, Vector3.down * distance, Color.red);

        inAir = Physics.Raycast(origin, Vector3.down, out RaycastHit hit, distance) ? false : true;
    }
}
