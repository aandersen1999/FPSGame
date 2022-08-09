using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Interactable
{
    //public DestroyType destroyType = DestroyType.AfterInteract;
    public string objectName = "Object";

    private const float spinSpeed = 180.0f;
    private readonly Vector3 spinner = new Vector3(0, spinSpeed);
    private GameObject model;

    #region monobehavior
    protected new void Awake()
    {
        base.Awake();
    }

    protected new void Start()
    {
        model = transform.GetChild(0).gameObject;
        float value;

        if (transform.parent != null)
        {
            transform.parent = null;
        }

        if(ReturnY(out value))
        {
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
        }
        else
        {
            Destroy(gameObject);
        }
        
        base.Start();
    }

    protected new void Update()
    {
        model.transform.localEulerAngles += spinner * Time.deltaTime;
    }
    #endregion

    public override void Interact()
    {


        base.Interact();
    }

    public override string GetInteractText()
    {
        return $"{interactText} {objectName}";
    }

    private bool ReturnY(out float value)
    {
        value = 0f;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hit))
        {
            value = hit.point.y;
            return true;
        }
        return false;
    }
}
