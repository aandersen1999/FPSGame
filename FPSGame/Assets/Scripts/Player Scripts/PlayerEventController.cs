using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventController : MonoBehaviour
{
    public bool toggleCrouch = false;
    public bool toggleRun = false;
    public bool active = true;
    #region Events
    public delegate void Action();

    public event Action OnPressAttack;
    public event Action OnPressReload;
    public event Action OnPressDrop;
    public event Action OnPressRun;
    public event Action OnPressJump;
    public event Action OnPressCrouch;
    public event Action OnPressNext;
    //public event Action OnPressPrev;
    public event Action OnPressInteract;

    #endregion

    #region Keys
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode reloadKey = KeyCode.R;
    public KeyCode dropKey = KeyCode.Q;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;
    //Temporary key
    public KeyCode nextKey = KeyCode.Tab;
    public KeyCode pauseKey = KeyCode.Escape;
    #endregion

    private void Awake()
    {
        EventManager.instance.PEC = this;
    }

    private void Update()
    {
        active = !GameMasterBehavior.Instance.Paused;
        if (active)
        {
            if (Input.GetKey(attackKey)) { OnPressAttack?.Invoke(); }

            if (Input.GetKeyDown(reloadKey)) { OnPressReload?.Invoke(); }
            if (Input.GetKeyDown(jumpKey)) { OnPressJump?.Invoke(); }
            if (Input.GetKeyDown(dropKey)) { OnPressDrop?.Invoke(); }
            if (Input.GetKeyDown(interactKey))
            {
                OnPressInteract?.Invoke();
            }
            if (Input.GetKeyDown(nextKey)) { OnPressNext?.Invoke(); }

            if (toggleCrouch)
            {
                if (Input.GetKeyDown(crouchKey))
                {
                    OnPressCrouch?.Invoke();
                }
            }
            else
            {
                if (Input.GetKeyDown(crouchKey))
                {
                    OnPressCrouch?.Invoke();
                }
                if (Input.GetKeyUp(crouchKey))
                {
                    OnPressCrouch?.Invoke();
                }
            }
            if (toggleRun)
            {
                if (Input.GetKeyDown(runKey))
                {
                    OnPressRun?.Invoke();
                }
            }
            else
            {
                if (Input.GetKeyDown(runKey))
                {
                    OnPressRun?.Invoke();
                }
                if (Input.GetKeyUp(runKey))
                {
                    OnPressRun?.Invoke();
                }
            }
        }
        if (Input.GetKeyDown(pauseKey))
        {
            GameMasterBehavior.Instance.TriggerPause();
        }
    }
}
