using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void Handler();
    public static event Handler AIEventTrigger;

    public bool AI_EventTriggerOn = true;

    private const float Long_Timer = .35f;

    private void OnEnable()
    {
        StartCoroutine(ActivateTimer());
    }

    private void OnDisable()
    {
        StopCoroutine(ActivateTimer());
    }

    private IEnumerator ActivateTimer()
    {
        while (AI_EventTriggerOn)
        {
            yield return new WaitForSeconds(Long_Timer);
            AIEventTrigger?.Invoke();
        }
    }
}
