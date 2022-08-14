using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour
{
    #region Events
    public delegate void Quotes(QuotesEventArgs e);
    public delegate void Health(HealthEventArgs e);

    public static event Health UpdateHealth;
    public static event Quotes UpdateQuotes;
    #endregion

    public static GUIScript instance;
    public Image pauseMenu;

    public List<string> quotes;
    public bool quotesFound = false;

    private void Awake()
    {
        instance = this;
        //quotesFound = HandleTextFile.GetQuotes(out quotes);
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        EventManager.WaveWarmUp += TriggerQuoteEvent;
        GameMasterBehavior.OnPause += PauseEvent;
    }

    private void OnDisable()
    {
        EventManager.WaveWarmUp -= TriggerQuoteEvent;
        GameMasterBehavior.OnPause -= PauseEvent;
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

    private void PauseEvent(bool pause)
    {
        if (pause)
        {
            pauseMenu.gameObject.SetActive(true);
        }
        else
        {
            pauseMenu.gameObject.SetActive(false);
        }
    }

    public void Resume()
    {
        GameMasterBehavior.Instance.TriggerPause();
    }

    public void BackToMainMenu()
    {
        GameMasterBehavior.OnPause -= PauseEvent;
        GameMasterBehavior.Instance.TriggerPause();
        GameMasterBehavior.Instance.BackToMainMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
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
