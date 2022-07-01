using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Events
    public delegate void UpdateQuotes(string s);

    public static event UpdateQuotes UpQuotes;
    #endregion

    public static EventManager instance;
    public PlayerEventController PEC;

    public delegate void Handler();
    public static event Handler AIEventLongTrigger;

    public bool AI_LongEventTriggerOn = true;

    private const float Short_Timer = .35f;
    private const float Long_Timer = .7f;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        StartCoroutine(ActivateLongTimer());
    }

    private void OnDisable()
    {
        StopCoroutine(ActivateLongTimer());
    }

    public static void TriggerQuoteEvent(string s)
    {
        UpQuotes?.Invoke(s);
    }

    private IEnumerator ActivateLongTimer()
    {
        while (AI_LongEventTriggerOn)
        {
            yield return new WaitForSeconds(Long_Timer);
            AIEventLongTrigger?.Invoke();
        }
    }
}
