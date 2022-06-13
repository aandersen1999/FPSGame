using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventController : MonoBehaviour
{
    #region Events
    public delegate void Action();

    public event Action OnPressAttack;
    public event Action OnPressReload;
    public event Action OnPressDrop;
    public event Action OnPressRun;
    public event Action OnPressJump;
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
    //Temporary key
    public KeyCode nextKey = KeyCode.Tab;
    #endregion


    private void Start()
    {
        EventManager.instance.PEC = this;
    }

    private void Update()
    {
        if (Input.GetKey(attackKey)) { OnPressAttack?.Invoke(); }
        if (Input.GetKey(runKey)) { OnPressRun?.Invoke(); }

        if (Input.GetKeyDown(reloadKey)) { OnPressReload?.Invoke(); }
        if (Input.GetKeyDown(jumpKey)) { OnPressJump?.Invoke(); }
        if (Input.GetKeyDown(dropKey)) { OnPressDrop?.Invoke(); }
        if (Input.GetKeyDown(interactKey)) 
        { 
            OnPressInteract?.Invoke();
        }
        if (Input.GetKeyDown(nextKey)) { OnPressNext?.Invoke(); }
    }
}
