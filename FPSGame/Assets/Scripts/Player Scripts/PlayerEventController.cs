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
    public KeyCode runKey = EditControls.binds[Keys.Run];
    public KeyCode attackKey = EditControls.binds[Keys.Attack];
    public KeyCode reloadKey = EditControls.binds[Keys.Reload];
    public KeyCode dropKey = EditControls.binds[Keys.Drop];
    public KeyCode interactKey = EditControls.binds[Keys.Use];
    public KeyCode jumpKey = EditControls.binds[Keys.Jump];
    public KeyCode crouchKey = EditControls.binds[Keys.Crouch];
    //Temporary key
    public KeyCode nextKey = EditControls.binds[Keys.Swap];
    public KeyCode pauseKey = EditControls.binds[Keys.Pause];
    #endregion

    private void Awake()
    {
        EventManager.instance.PEC = this;

        runKey = EditControls.binds[Keys.Run];
        attackKey = EditControls.binds[Keys.Attack];
        reloadKey = EditControls.binds[Keys.Reload];
        dropKey = EditControls.binds[Keys.Drop];
        interactKey = EditControls.binds[Keys.Use];
        jumpKey = EditControls.binds[Keys.Jump];
        crouchKey = EditControls.binds[Keys.Crouch];
        nextKey = EditControls.binds[Keys.Swap];
        pauseKey = EditControls.binds[Keys.Pause];
    }

    private void OnEnable()
    {
        GameMasterBehavior.OnPause += PauseEvent;
    }

    private void OnDisable()
    {
        GameMasterBehavior.OnPause -= PauseEvent;
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

    private void PauseEvent(bool pause)
    {
        active = !pause;
    }

}