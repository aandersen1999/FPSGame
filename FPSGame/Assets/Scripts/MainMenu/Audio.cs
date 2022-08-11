using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public Slider volume;

    private float beforeEdit;

    private void OnEnable()
    {
        beforeEdit = AudioListener.volume;
        volume.value = AudioListener.volume;
    }

    public void Apply()
    {
        gameObject.SetActive(false);
    }

    public void Discard()
    {
        AudioListener.volume = beforeEdit;
        gameObject.SetActive(false);
    }

    public void OnEdit()
    {
        AudioListener.volume = volume.value;
    }
}
