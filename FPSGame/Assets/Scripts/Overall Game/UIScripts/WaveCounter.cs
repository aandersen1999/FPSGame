using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCounter : MonoBehaviour
{
    public Color initialColor;
    public Color changeColor;
    private Text t;

    private const float transTime = 2.0f;

    private void Awake()
    {
        t = GetComponent<Text>();
        t.color = initialColor;
        t.text = $"{GameMasterBehavior.Instance.waveNum}";
    }

    private void OnEnable()
    {
        EventManager.StopWave += UpdateNum;
    }

    private void OnDisable()
    {
        EventManager.StopWave -= UpdateNum;
    }

    private void UpdateNum()
    {
        StartCoroutine(change());
    }

    private IEnumerator change()
    {
        for(float timeReference = 0.0f; timeReference < transTime; timeReference += Time.deltaTime)
        {
            float time = timeReference / transTime;
            Color referenceCol = Color.Lerp(initialColor, changeColor, time);
            t.color = referenceCol;
            yield return null;
        }
        t.text = $"{GameMasterBehavior.Instance.waveNum}";
        for (float timeReference = 0.0f; timeReference < transTime; timeReference += Time.deltaTime)
        {
            float time = timeReference / transTime;
            Color referenceCol = Color.Lerp(changeColor, initialColor, time);
            t.color = referenceCol;
            yield return null;
        }
    }
}
