using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseAbyss : MonoBehaviour
{
    private bool growing = true;

    private RectTransform rect;

    private readonly Vector3 changer = new Vector3(.02f, .02f, 0);
    private readonly Vector3 defaultSize = new Vector3(1.5f, 1.5f, 0);
    private readonly Vector3 maxSize = new Vector3(2.5f, 2.5f, 0);
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        growing = true;
        rect.localScale = defaultSize;
    }

    private void Update()
    {
        if (growing)
        {
            rect.localScale += changer * Time.unscaledDeltaTime;
            if(rect.localScale.x > maxSize.x)
            {
                rect.localScale = maxSize;
                growing = false;
            }
        }
        else
        {
            rect.localScale -= changer * Time.unscaledDeltaTime;
            if (rect.localScale.x < defaultSize.x)
            {
                rect.localScale = defaultSize;
                growing = true;
            }
        }
    }
}
