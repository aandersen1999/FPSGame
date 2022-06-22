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

    private float verticalVelocity;
    private const float gravity = -15.0f;
    private const float terminalVelocity = -100.0f;

    private bool inAir = true;
    private bool canStand = true;
    #endregion

    private PlayerEventController pec;
    private CharacterController cc;
    //private CapsuleCollider box;
    //private Rigidbody rb;

    private bool WTU_Running = false;

    #region Monobehavior
    private void Awake()
    {
        pec = GetComponent<PlayerEventController>();
        cc = GetComponent<CharacterController>();
        //box = GetComponent<CapsuleCollider>();
        //rb = GetComponent<Rigidbody>();

        cam.fieldOfView = fov;
        camTransform = cam.transform;

        GameMasterBehavior.GameMaster.playerObject = gameObject;
        GameMasterBehavior.GameMaster.playerController = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
            //Vector3 movement = (!inAir) ? new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) :
            //new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (inAir)
            {
                verticalVelocity += gravity * Time.deltaTime;
                verticalVelocity = Mathf.Clamp(verticalVelocity, terminalVelocity, -terminalVelocity);
            }
            movement = transform.TransformDirection(movement.normalized);
            cc.Move(movement * Time.deltaTime * walkSpeed);
            cc.Move(new Vector3(0, verticalVelocity * Time.deltaTime, 0));
        }
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
            else if (!WTU_Running)
            {
                StartCoroutine(WaitToUncrouch());
            }
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

    /*private void CheckForGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + (transform.localScale.y * .1f), transform.position.z);
        float distance = .75f;
        Debug.DrawRay(origin, Vector3.down * distance, Color.red);

        inAir = Physics.Raycast(origin, Vector3.down, out RaycastHit hit, distance) ? false : true;
    }*/
    
    //checks above head to see if you should be able to uncrouch
    private void CheckAboveHead()
    {
        float distance = 1.5f;

        Debug.DrawRay(cam.transform.position, Vector3.up * distance, Color.green);
        canStand = Physics.Raycast(cam.transform.position, Vector3.up, out RaycastHit hit, distance) ? false : true;
    }
    #endregion

    private IEnumerator WaitToUncrouch()
    {
        WTU_Running = true;
        while (!canStand)
        {
            yield return null;
        }
        Crouch();
        WTU_Running = false;
    }
}
