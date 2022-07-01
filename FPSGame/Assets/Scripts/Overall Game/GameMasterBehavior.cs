using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterBehavior : MonoBehaviour
{
    public static GameMasterBehavior GameMaster { get; private set; }
    public static LayerMask ObjectLayer { get; private set; }

    public GameObject playerObject;
    public Player playerController;

    public List<string> quotes;

    public bool quotesFound = false;

    private void Awake()
    {
        GameMaster = this;
        quotesFound = HandleTextFile.GetQuotes(out quotes);
    }

    private void Start()
    {
        ObjectLayer = LayerMask.GetMask("Object");
    }

    private void Update()
    {
        if (playerObject != null)
        {
            EnemyScript.playerPosition = playerObject.transform.position + Vector3.down;
        }

        if (Input.GetKeyUp(KeyCode.N))
        {
            EventManager.TriggerQuoteEvent("Test");
        }
    }
}
