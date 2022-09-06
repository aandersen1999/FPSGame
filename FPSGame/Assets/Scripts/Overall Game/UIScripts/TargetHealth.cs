using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetHealth : MonoBehaviour
{
    private Slider healthSlider;
    private EnemyTarget target;

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();  
    }

    private void Start()
    {
        target = GameMasterBehavior.Instance.enemyTarget;
        healthSlider.maxValue = target.health;
    }

    private void Update()
    {
        if(target != null)
        {
            healthSlider.value = target.health;
        }
        else
        {
            healthSlider.value = 0;
        }
    }
}
