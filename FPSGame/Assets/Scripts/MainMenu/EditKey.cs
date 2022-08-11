using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditKey : MonoBehaviour
{
    public Keys keyReference;

    public Text KeyName;
    public Text KeyValue;

    public EditControls controls;

    private void Start()
    {
        KeyName.text = keyReference.ToString();
        KeyValue.text = controls.MenuKeys[keyReference].ToString();
        controls.Edit += UpdateSelf;
    }

    private void OnDisable()
    {
        controls.Edit -= UpdateSelf;
        Destroy(gameObject);
    }

    private void UpdateSelf()
    {
        KeyValue.text = controls.MenuKeys[keyReference].ToString();
    }
}
