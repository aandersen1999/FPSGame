using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuotesUI : MonoBehaviour
{
    public float opacityLossPS = 0.2f;
    public float secondsBeforeFade = 4.0f;

    private string output = "";
    private float opacity = 0.0f;
    
    private Text textAsset;

    private bool isActive = false;

    private void Awake()
    {
        textAsset = GetComponent<Text>();
        textAsset.text = output;
        opacity = 0.0f;
        textAsset.color = new Color(textAsset.color.r, textAsset.color.g, textAsset.color.b, opacity);
    }

    private void OnEnable()
    {
        GUIScript.UpdateQuotes += DisplayQuote;
    }

    private void OnDisable()
    {
        GUIScript.UpdateQuotes -= DisplayQuote;
    }

    private void DisplayQuote(QuotesEventArgs e)
    {
        if (!isActive && e.quote != null)
        {
            textAsset.text = e.quote;
            StartCoroutine(Display());
        }
    }

    private IEnumerator Display()
    {
        isActive = true;
        opacity = 1.0f;

        textAsset.color = new Color(textAsset.color.r, textAsset.color.g, textAsset.color.b, opacity);
        yield return new WaitForSeconds(secondsBeforeFade);

        while (opacity > 0.0f)
        {
            opacity -= opacityLossPS * Time.deltaTime;
            Mathf.Max(opacity, 0.0f);

            textAsset.color = new Color(textAsset.color.r, textAsset.color.g, textAsset.color.b, opacity);
            yield return null;
        }
        isActive = false;
    }
}
