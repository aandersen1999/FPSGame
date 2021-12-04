using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string itemName = "Unknown";

    //The prefab that is used on the player when the item is picked up
    public GameObject heldItem;

    private GameObject childObject;

    private float spinSpeed = 5f;
    

    private void Start()
    {
        childObject = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        childObject.transform.localEulerAngles += new Vector3(0, spinSpeed);
    }
        
}
