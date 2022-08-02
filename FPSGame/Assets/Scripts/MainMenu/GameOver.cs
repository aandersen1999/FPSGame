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
        SceneManager.LoadScene(0);
    }
}
