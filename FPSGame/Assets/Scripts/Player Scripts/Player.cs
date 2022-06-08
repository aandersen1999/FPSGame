using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100.0f;

    #region Camera
    public Camera cam;
    public Light eyeSight;
    public WeaponHandBehavior weaponHand;

    public bool lockCamera = false;
    public bool lockMovement = false;

    [Range(60.0f, 100.0f)]
    public float fov = 60.0f;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 70.0f;
    public float eyeSightRange = 7.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private readonly float checkObjectRange = 1.5f;
    #endregion

    #region Movement
    public float walkSpeed = 4.0f;
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
    }

    private void Start()
    {
        GameMasterBehavior.GameMaster.playerObject = gameObject;
        eyeSight.range = eyeSightRange;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
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
        }
    }
    #endregion
}
