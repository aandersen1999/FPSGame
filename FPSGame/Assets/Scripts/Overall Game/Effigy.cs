using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effigy : Interactable
{
    public Light candleLight;
    public MeshRenderer candleRender;

    private float transitionTime = 5.0f;

    private Material FireMaterial;

    private const int EmissionColorID = 95;

    private readonly Color NormalFire = new Color32(250, 166, 45, 255);
    private readonly Color NormalEmission = new Color32(191, 0, 0, 255);
    private readonly Color TriggeredFire = new Color32(96, 45, 250, 255);
    private readonly Color TriggeredEmission = new Color32(44, 0, 136, 255);

    private bool midTransition = false;

    protected new void Awake()
    {
        base.Awake();
    }

    protected new void Start()
    {
        base.Start();
        FireMaterial = candleRender.materials[1];
    }

    private void OnEnable()
    {
        StartCoroutine(Flicker());
        EventManager.WaveWarmUp += ToggleEffigy;
        EventManager.StopWave += ToggleEffigy;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        EventManager.WaveWarmUp -= ToggleEffigy;
        EventManager.StopWave -= ToggleEffigy;
    }

    protected new void Update()
    {
        base.Update();

    }

    public override void Interact()
    {
        if (canInteract)
        {
            EventManager.instance.StartWaveTrigger();
        }
        base.Interact();
    }

    private void ToggleEffigy()
    {
        if (!midTransition)
        {
            StartCoroutine(ChangeColor());

            canInteract = !canInteract;
        }
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            candleLight.intensity += (Random.Range(0, 2) == 1) ? -.04f : .04f;
            candleLight.intensity = Mathf.Clamp(candleLight.intensity, 0.5f, 0.9f);

            yield return new WaitForSeconds(.17f);
        }
    }

    private IEnumerator ChangeColor()
    {

        midTransition = true;
        if (canInteract)
        {
            for (float timeReference = 0.0f; timeReference < transitionTime; timeReference += Time.deltaTime)
            {
                //Just so I don't have to calculate this stuff more than once a frame
                float timeRatio = timeReference / transitionTime;
                Color refrenceColor = Color.Lerp(NormalFire, TriggeredFire, timeRatio);

                FireMaterial.color = refrenceColor;
                candleLight.color = refrenceColor;
                FireMaterial.SetColor(EmissionColorID, Color.Lerp(NormalEmission, TriggeredEmission, timeRatio));
                yield return null;
            }
        }
        else
        {
            for (float timeReference = 0.0f; timeReference < transitionTime; timeReference += Time.deltaTime)
            {

                float timeRatio = timeReference / transitionTime;
                Color refrenceColor = Color.Lerp(TriggeredFire, NormalFire, timeRatio);

                FireMaterial.color = refrenceColor;
                candleLight.color = refrenceColor;
                FireMaterial.SetColor(EmissionColorID, Color.Lerp(TriggeredEmission, NormalEmission, timeRatio));
                yield return null;
            }
        }
        midTransition = false;
    }
}
