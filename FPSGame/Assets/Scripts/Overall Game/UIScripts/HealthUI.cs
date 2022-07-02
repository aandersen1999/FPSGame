using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Color UIColor;

    public Text healthText;
    public RawImage image;

    private readonly Color normalColor = new Color(1.0f, .57f, 0.0f, .65f);
    private readonly Color dangerColor = new Color(.78f, 0.0f, 0.0f, .65f);
    private readonly Color deadColor = new Color(.16f, 0.0f, 0.0f, .65f);

    private void OnEnable()
    {
        GUIScript.UpdateHealth += DisplayHealth;
    }

    private void OnDisable()
    {
        GUIScript.UpdateHealth -= DisplayHealth;
    }

    private void DisplayHealth(HealthEventArgs e)
    {
        int displayHealth = (int)e.health;

        if (e.dead)
        {
            UIColor = deadColor;
        }
        else
        {
            UIColor = (displayHealth <= 25) ? dangerColor : normalColor;
        }
        
        healthText.text = displayHealth.ToString();
        healthText.color = UIColor;
        image.color = UIColor;

    }
}
