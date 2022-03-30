using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectreBehavior : MonoBehaviour
{
    public float defaultMovementSpeed = 0.01f;
    private float movementSpeed;

    public float lightIntensity = 2.0f;

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
        movementSpeed = defaultMovementSpeed * EnemyScript.hordeAgression;
        StartCoroutine(GhostLightSpawn());
    }

    private void Update()
    {
        transform.LookAt(EnemyScript.playerPosition);
    }

    private void FixedUpdate()
    {
        transform.position += transform.TransformDirection(Vector3.forward) * movementSpeed;
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
