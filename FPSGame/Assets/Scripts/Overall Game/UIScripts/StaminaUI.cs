using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    private Slider meter;

    private void Awake()
    {
        meter = GetComponent<Slider>();
    }

    private void Update()
    {
        meter.value = GameMasterBehavior.Instance.playerController.stamina;
    }

    public void ChangeMaxStamina(float max)
    {
        meter.maxValue = max;
    }
}
