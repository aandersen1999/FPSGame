using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text score;

    public void MainMenu()
    {
        int coin = Random.Range(0, 2);
        if(coin == 0)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(3);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }
}
