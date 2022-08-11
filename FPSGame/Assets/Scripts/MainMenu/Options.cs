using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public GameObject ControlMenu;
    public GameObject AudioMenu;

    public void OpenAudio()
    {
        AudioMenu.SetActive(true);
    }

    public void OpenControls()
    {
        ControlMenu.SetActive(true);
    }

    public void Return()
    {
        gameObject.SetActive(false);
    }
}
