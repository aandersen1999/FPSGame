using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIScript : MonoBehaviour
{
    #region Events
    public delegate void Quotes(QuotesEventArgs e);
    public delegate void Health(HealthEventArgs e);

    public static event Health UpdateHealth;
    public static event Quotes UpdateQuotes;
    #endregion

    public static GUIScript instance;

    public List<string> quotes;
    public bool quotesFound = false;

    private void Awake()
    {
        instance = this;
        quotesFound = HandleTextFile.GetQuotes(out quotes);
    }

    private void Start()
    {

    }

    public void TriggerQuoteEvent()
    {
        if (quotesFound)
        {
            QuotesEventArgs args = new QuotesEventArgs();

            args.quote = quotes[UnityEngine.Random.Range(0, quotes.Count)];

            UpdateQuotes?.Invoke(args);
        }
        else
        {
            Debug.LogWarning("No Quotes to display");
        }
    }

    public void TriggerHealth(float _health, bool _dead)
    {
        HealthEventArgs args = new HealthEventArgs();
        args.health = _health;
        args.dead = _dead;
        UpdateHealth?.Invoke(args);
    }
}

public struct HealthEventArgs
{
    public float health { get; set; }
    public bool dead { get; set; }
}

public struct QuotesEventArgs
{
    public string quote { get; set; }
}
