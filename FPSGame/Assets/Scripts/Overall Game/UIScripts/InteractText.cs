using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractText : MonoBehaviour
{
    private Text t;
    private bool visible;

    private void Awake()
    {
        t = GetComponent<Text>();
        t.color = new Color32(255, 245, 59, 0);
    }

    private void Start()
    {
        if(EventManager.instance.PEC != null)
        {
            t.text = "Press " + EventManager.instance.PEC.interactKey.ToString() + " to use";
        }
        else
        {
            t.text = "Press E to use";
        }
    }

    private void Update()
    {
        ToggleText(GameMasterBehavior.Instance.playerController.InteractableObject != null);
    }

    private void ToggleText(bool toggle)
    {
        if (toggle)
        {
            if (!visible) 
            {
                t.color = new Color32(255, 245, 59, 255);
                visible = true;
            }
        }
        else
        {
            if (visible)
            {
                t.color = new Color32(255, 245, 59, 0);
                visible = false;
            }
        }
    }
}
