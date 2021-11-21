using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode attackKey = KeyCode.Mouse0;

    public WeaponBehavior_Melee weapon;
    public StaminaState staminaState;

    #region Camera Variables
    public Camera playerCamera;

    public bool lockedCamera = false;

    [Range(60.0f, 100.0f)]
    public float fov = 60f;
    public float mouseSensititvity = 2f;
    public float maxLookAngle = 70f;

    private float yaw = 0f;
    private float pitch = 0f;
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

    public bool isRecovering;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        playerCamera.fieldOfView = fov;

        weapon.playerController = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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

        if (Input.GetKeyDown(attackKey))
        {
            weapon.attack();
        }
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

            if(staminaState == StaminaState.tired) { targetVelocity = targetVelocity * .6f; }

            
            Vector3 velocityChange = (targetVelocity - rb.velocity);
            velocityChange.y = 0;

            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }

    private StaminaState CalculateStaminaState()
    {
        float ratio = stamina / maxStamina;

        if(ratio >= .67) { return StaminaState.fine; }
        else if(ratio >= .33) { return StaminaState.winded; }
        else { return StaminaState.tired; }
    }
}

public enum StaminaState : byte { fine, winded, tired }
