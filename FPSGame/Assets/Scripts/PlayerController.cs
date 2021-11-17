using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    #region Camera Variables
    public Camera playerCamera;

    public bool lockedCamera = false;

    public float fov = 60f;
    public float mouseSensititvity = 2f;
    public float maxLookAngle = 50f;

    private float yaw = 0f;
    private float pitch = 0f;
    #endregion

    public bool lockMovement = false;
    public float walkSpeed = 5f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        playerCamera.fieldOfView = fov;

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!lockedCamera)
        {
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensititvity;
            pitch -= mouseSensititvity * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(0, yaw, 0);
            playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        if (!lockMovement)
        {
            Vector3 targetVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            targetVelocity = transform.TransformDirection(targetVelocity) * walkSpeed;
            
            Vector3 velocityChange = targetVelocity - rb.velocity;
            rb.AddForce(velocityChange, ForceMode.VelocityChange);

        }
    }
}
