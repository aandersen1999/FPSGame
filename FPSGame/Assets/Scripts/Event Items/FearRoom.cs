using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearRoom : MonoBehaviour
{
    private const float Time = 142.0f;

    private void Start()
    {
        StartCoroutine(Timer());
        HandleTextFile.WriteFearFile();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(Time);
        Application.Quit();
    }
}
