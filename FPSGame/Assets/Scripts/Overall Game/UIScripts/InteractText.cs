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
        t.color = new Color32(50, 50, 50, 0);
    }

    private void Start()
    {
        t.text = "Press " + EventManager.instance.PEC.interactKey.ToString() + " to use";
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
                t.color = new Color32(50, 50, 50, 255);
                visible = true;
            }
        }
        else
        {
            if (visible)
            {
                t.color = new Color32(50, 50, 50, 0);
                visible = false;
            }
        }
    }
}
