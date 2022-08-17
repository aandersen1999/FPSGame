using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private Text t;
    private float deltaTime = 0.0f;
    private float timer = .5f;

    private void Awake()
    {
        t = GetComponent<Text>();
    }

    private void OnEnable()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            deltaTime += (Time.deltaTime - deltaTime) * .1f;
            float display = 1.0f / deltaTime;
            t.text = $"FPS: {Mathf.Ceil(display)}";
            yield return new WaitForSeconds(timer);
        }
    }
}
