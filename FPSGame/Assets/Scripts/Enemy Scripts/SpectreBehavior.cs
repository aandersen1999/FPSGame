using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectreBehavior : MonoBehaviour
{
    public float defaultMovementSpeed = 0.01f;
    private float movementSpeed;

    public float lightIntensity = 2.0f;
    public SpectreState state = SpectreState.Approach;

    private EnemyScript script;
    public Light ghostLight;

    private const float secondsTillLit = 5.0f;

    #region Monobehavior
    private void Awake()
    {
        script = GetComponent<EnemyScript>();

    }
    private void OnEnable()
    {
        EventManager.AIEventLongTrigger += DecisionTree;

        movementSpeed = defaultMovementSpeed * EnemyScript.hordeAgression;
        StartCoroutine(GhostLightSpawn());

    }

    private void OnDisable()
    {
        EventManager.AIEventLongTrigger -= DecisionTree;
    }

    private void Update()
    {
        //transform.LookAt(EnemyScript.playerPosition);
        
    }

    
    #endregion

    private void DecisionTree()
    {
        if (Vector3.Distance(EnemyScript.playerPosition, transform.position) > 4.0f)
        {
            if(Random.value > .05f)
            {
                state = SpectreState.Approach;
            }
            else
            {
                state = SpectreState.Teleport;
            }
        }
        else
        {
            if(state == SpectreState.StrafingLeft)
            {
                if(Random.value < .1f)
                {
                    state = SpectreState.StrafingRight;
                }
            }
            else if(state == SpectreState.StrafingRight)
            {
                if (Random.value < .1f)
                {
                    state = SpectreState.StrafingLeft;
                }
            }
            else
            {
                if(Random.value > .5f)
                {
                    state = SpectreState.StrafingRight;
                }
                else
                {
                    state = SpectreState.StrafingLeft;
                }
            }
        }
    }

    #region Decisions
    private void Teleport()
    {
        transform.position = EnemyScript.playerPosition;
        state = SpectreState.Idle;
    }
    #endregion

    private IEnumerator GhostLightSpawn()
    {
        float lightPerSecond = lightIntensity / secondsTillLit;

        ghostLight.intensity = 0.0f;

        while (ghostLight.intensity < lightIntensity)
        {
            ghostLight.intensity += lightPerSecond * Time.deltaTime;
            ghostLight.intensity = Mathf.Min(ghostLight.intensity, lightIntensity);

            yield return null;
        }
    }
}

public enum SpectreState : byte
{
    Idle,
    Approach,
    StrafingLeft,
    StrafingRight,
    Teleport,
    Attacking
}
