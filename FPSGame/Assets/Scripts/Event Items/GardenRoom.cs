using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GardenRoom : MonoBehaviour
{
    public Transform playerTransform;

    private bool flagX = false;
    private float time = 80.0f;

    private void Start()
    {
        StartCoroutine(Timer());
    }

    private void Update()
    {
        if (Mathf.Abs(playerTransform.position.x) >= 10.0f)
        {
            flagX = true;
        }
        if (Mathf.Abs(playerTransform.position.z) >= 10.0f)
        {
            flagX = true;
        }
    }

    private void LateUpdate()
    {
        if (flagX)
        {
            playerTransform.position = new Vector3(0, playerTransform.position.y, 0);
        }
        
        flagX = false;
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(0);
    }
}
