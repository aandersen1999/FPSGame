using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Events
    public delegate void Action();
    public static event Action OnPressAttack;
    public static event Action OnPressReload;
    public static event Action OnPressDrop;
    public static event Action OnPressNext;
    public static event Action OnPressPrev;
    #endregion

    private Rigidbody rb;

    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode reloadKey = KeyCode.R;
    public KeyCode dropKey = KeyCode.Q;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode jumpKey = KeyCode.Space;

    public WeaponHandBehavior weaponHand;
    public StaminaState staminaState;
    public GameObject InteractableObject;

    #region Camera Variables
    public Camera playerCamera;
    public Light EyeSight;

    public bool lockedCamera = false;

    [Range(60.0f, 100.0f)]
    public float fov = 60f;
    public float mouseSensititvity = 2f;
    public float maxLookAngle = 70f;
    public float eyeSightRange = 7.0f;

    private float yaw = 0f;
    private float pitch = 0f;

    private float checkObjectRange = 1.5f;
    #endregion

    #region Movement Variables
    public bool lockMovement = false;
    public float walkSpeed = 4f;
    public float runSpeed = 8.5f;

    public float maxStamina = 300f;
    public float stamina = 300f;

    readonly float runStaminaDrain = 1f;
    readonly float runStaminaGain = .5f;
    #endregion

    public float jumpHeight = 5f;
    public bool inAir = true;

    public bool isRecovering;

    #region MonoBehavior
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        playerCamera.fieldOfView = fov;
    }

    private void Start()
    {
        GameMasterBehavior.GameMaster.playerObject = gameObject;

        weaponHand = GetComponentInChildren<WeaponHandBehavior>();
        weaponHand.playerCont = this;
        EyeSight.range = eyeSightRange;
    }

    private void Update()
    {
        staminaState = CalculateStaminaState();

        if (!lockedCamera)
        {
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensititvity;
            pitch -= mouseSensititvity * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.eulerAngles = new Vector3(0, yaw, 0);
            playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }

        if (Input.GetKeyDown(jumpKey))
        {
            if (!inAir)
            {
                rb.AddForce(0f, jumpHeight, 0f, ForceMode.Impulse);
                inAir = true;
            }
        }
        
        if (Input.GetKey(attackKey)) { OnPressAttack?.Invoke(); }
        if (Input.GetKeyDown(reloadKey)) { OnPressReload?.Invoke(); }
        if (Input.GetKeyDown(dropKey)) { OnPressDrop?.Invoke(); }
        if (Input.GetKeyDown(interactKey))
        {
            if (InteractableObject != null)
            {
                if (InteractableObject.GetComponent<InteractableWeapon>() != null)
                {
                    InteractableObject.GetComponent<InteractableWeapon>().PickUpWeapon();
                }
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0) { OnPressNext?.Invoke(); }
            else { OnPressPrev?.Invoke(); }
        }

        CheckForGround();
    }

    private void FixedUpdate()
    {
        if (!lockMovement)
        {
            Vector3 targetVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));


            if (Input.GetKey(runKey))
            {
                targetVelocity = stamina > 0 ? transform.TransformDirection(targetVelocity.normalized) * runSpeed :
                    transform.TransformDirection(targetVelocity.normalized) * walkSpeed;

                stamina -= runStaminaDrain;
            }
            else
            {
                targetVelocity = transform.TransformDirection(targetVelocity.normalized) * walkSpeed;

                stamina += runStaminaGain;
            }
            stamina = Mathf.Clamp(stamina, 0, maxStamina);

            if (staminaState == StaminaState.tired) { targetVelocity = targetVelocity * .6f; }


            Vector3 velocityChange = (targetVelocity - rb.velocity);
            velocityChange.y = 0;

            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
        CheckForObject();
    }
    #endregion

    //Not sure if I'm gonna keep this and rework it, but for now I'm just gonna leave it in here unactivated
    public void KickBack(float kickBack)
    {
        transform.eulerAngles += new Vector3(0, Random.Range(-kickBack, kickBack), 0);
        playerCamera.transform.localEulerAngles += new Vector3(Random.Range(-kickBack, kickBack), 0, 0);
    }

    private StaminaState CalculateStaminaState()
    {
        float ratio = stamina / maxStamina;

        if (ratio >= .67) { return StaminaState.fine; }
        else if (ratio >= .33) { return StaminaState.winded; }
        else { return StaminaState.tired; }
    }

    private void CheckForGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .4f), transform.position.z);
        float distance = .75f;
        Debug.DrawRay(origin, Vector3.down * distance, Color.red);

        inAir = Physics.Raycast(origin, Vector3.down, out RaycastHit hit, distance) ? false : true;
    }

    private void CheckForObject()
    {
        InteractableObject = null;

        RaycastHit checker;

        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward) * checkObjectRange, Color.yellow);
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out checker, checkObjectRange, GameMasterBehavior.ObjectLayer))
        {
            InteractableObject = checker.collider.gameObject;
        }
    }
}

public enum StaminaState : byte { fine, winded, tired }
