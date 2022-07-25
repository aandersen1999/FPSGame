using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    
    public PlayerEventController PEC;

    public delegate void Handler();

    public static event Handler AIEventLongTrigger;
    public static event Handler WaveWarmUp;
    public static event Handler StartWave;
    public static event Handler StopWave;

    public bool AI_LongEventTriggerOn = true;

    private const float Short_Timer = .35f;
    private const float Long_Timer = .7f;

    public bool waveActive = false;

    private bool warmUpActive = false;

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
        StopAllCoroutines();
    }

    private void Update()
    {

    }

    public void StartWaveTrigger()
    {
        if (!waveActive && !warmUpActive)
        {
            WaveWarmUp?.Invoke();
            StartCoroutine(WarmUpTransition());
        }
    }

    public void StopWaveTrigger()
    {
        if (waveActive)
        {
            waveActive = false;
            StopWave?.Invoke();
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

    private IEnumerator WarmUpTransition()
    {
        warmUpActive = true;

        yield return new WaitForSeconds(5.0f);
        StartWave?.Invoke();
        waveActive = true;

        warmUpActive = false;
    }

}