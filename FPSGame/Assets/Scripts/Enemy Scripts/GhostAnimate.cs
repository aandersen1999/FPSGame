using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimate : MonoBehaviour
{
    public GameObject[] meshes;
    public float secondsBetweenSwap;
    public bool animate;
    public bool randomAnimate;

    private short currentMeshIndex;

    private float opacity;
    private const float opacityPerSecond = .75f / 5.0f;

    private void Awake()
    {
        currentMeshIndex = 0;
        if(meshes != null)
        {
            foreach(GameObject mesh in meshes)
            {
                mesh.SetActive(false);
            }

            meshes[currentMeshIndex].SetActive(true);
        }
        else 
        {
            animate = false;
        }
    }

    private void OnEnable()
    {
        opacity = 0.0f;
        StartCoroutine(Animate());
    }

    private void OnDisable()
    {
        StopCoroutine(Animate());
    }

    private void Update()
    {
        if(meshes != null)
        {
            opacity += opacityPerSecond * Time.deltaTime;
            opacity = Mathf.Min(opacity, .75f);

            var col = meshes[currentMeshIndex].GetComponent<Renderer>().material.color;
            col.a = opacity;
            meshes[currentMeshIndex].GetComponent<Renderer>().material.color = col;
        }
    }

    private IEnumerator Animate()
    {
        while(animate)
        {
            if (randomAnimate)
            {
                int rand = Random.Range(0, meshes.Length);
                if(rand == currentMeshIndex)
                {
                    rand++;
                    if(rand == meshes.Length) { rand = 0; }
                }

                meshes[currentMeshIndex].SetActive(false);
                currentMeshIndex = (short)rand;
                meshes[currentMeshIndex].SetActive(true);

                yield return new WaitForSeconds(secondsBetweenSwap);
            }
            else
            {
                if (currentMeshIndex + 1 < meshes.Length)
                {
                    meshes[currentMeshIndex].SetActive(false);
                    currentMeshIndex++;
                    meshes[currentMeshIndex].SetActive(true);
                }
                else
                {
                    meshes[currentMeshIndex].SetActive(false);
                    currentMeshIndex = 0;
                    meshes[currentMeshIndex].SetActive(true);
                }

                yield return new WaitForSeconds(secondsBetweenSwap);
            }
        }
    }
}
