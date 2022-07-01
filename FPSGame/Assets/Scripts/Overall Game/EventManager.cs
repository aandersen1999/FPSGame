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

    public List<string> quotes;
    public bool quotesFound = false;

    private void Awake()
    {
        instance = this;
        quotesFound = HandleTextFile.GetQuotes(out quotes);
    }

    private void OnEnable()
    {
        StartCoroutine(ActivateLongTimer());
    }

    private void OnDisable()
    {
        StopCoroutine(ActivateLongTimer());
    }

    public void TriggerQuoteEvent()
    {
        if (quotesFound)
        {
            string returnQuote = quotes[Random.Range(0, quotes.Count)];

            UpQuotes?.Invoke(returnQuote);
        }
        else
        {
            Debug.LogWarning("No Quotes to display");
        }
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
