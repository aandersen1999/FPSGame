using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractText : MonoBehaviour
{
    private Text t;
    private bool visible;

    private string followUp;
    private string defaultText;

    private GameObject interactable;

    private void Awake()
    {
        t = GetComponent<Text>();
        t.color = new Color32(255, 245, 59, 0);
    }

    private void Start()
    {
        if(EventManager.instance.PEC != null)
        {
            followUp = $"Press {EventManager.instance.PEC.interactKey} ";
            defaultText = $"Press {EventManager.instance.PEC.interactKey} to use";
        }
        else
        {
            t.text = "Press E to use";
        }
    }

    private void Update()
    {
        bool toggle = GameMasterBehavior.Instance.playerController.InteractableObject != null;

        if (toggle)
        {
            bool changed = GetChanged(GameMasterBehavior.Instance.playerController.InteractableObject);
            if (changed || !visible)
            {
                t.color = new Color32(255, 245, 59, 255);
                visible = true;
                t.text = interactable.TryGetComponent(out Interactable reference) ?
                    followUp + reference.GetInteractText() : defaultText;
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

    private bool GetChanged(GameObject obj)
    {
        bool changed = interactable != obj;
        interactable = obj;
        return changed;
    }
}
