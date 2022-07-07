using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadGlitchy : MonoBehaviour
{
    private const float PositionLimit = .1f;
    private const float RotationLimit = 20.0f;

    public bool active = true;
    private bool routineOn = false;

    private Vector3 defaultPosition;
    private Vector3 defaultRotation;
    private float defaultX;

    private void Awake()
    {
        defaultPosition = transform.localPosition;
        defaultRotation = transform.localEulerAngles;
        defaultX = transform.localRotation.x;
    }

    private void Update()
    {
        if (active && !routineOn)
        {
            StartCoroutine(headRoutine());
        }
    }

    public void Toggle(bool toggle)
    {
        active = toggle;

        transform.localPosition = defaultPosition;
        transform.localEulerAngles = defaultRotation;
    }

    private IEnumerator headRoutine()
    {
        routineOn = true;
        while (active)
        {
            //Quaternion inst = new Quaternion();
            //inst.Set(0, Random.Range(-RotationYLimit, RotationYLimit), Random.Range(-RotationZLimit, RotationZLimit), 1);

            transform.localPosition = new Vector3(Random.Range(-PositionLimit, PositionLimit),
                                                  Random.Range(-PositionLimit, PositionLimit), 
                                                  defaultPosition.z);
            //transform.localEulerAngles = inst.eulerAngles;
            transform.localRotation = Quaternion.Euler(Random.Range(-RotationLimit + 90, RotationLimit + 90),
                                                       Random.Range(-RotationLimit, RotationLimit),
                                                       Random.Range(-RotationLimit, RotationLimit));

            yield return new WaitForSeconds(.05f);
        }
        routineOn = false;
        transform.localPosition = defaultPosition;
        transform.localEulerAngles = defaultRotation;
    }
}
