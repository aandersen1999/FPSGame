using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Image Panel;
    public Text EWBO;
    public AudioSource Music;

    public void StartGame()
    {
        StartCoroutine(StartGameSequence());
        StartCoroutine(QuietMusic());
    }

    public void QuitGame()
    {
        int coin = Random.Range(0, 2);

        if(coin == 0)
        {
            Application.Quit();
        }
        SceneManager.LoadScene(2);
    }

    private IEnumerator StartGameSequence()
    {
        Panel.raycastTarget = true;

        Color tmpP = Panel.color;
        Color tmpT = EWBO.color;

        while(tmpP.a < 1.0f)
        {
            tmpP.a += .5f * Time.deltaTime;
            tmpP.a = Mathf.Min(tmpP.a, 1.0f);
            Panel.color = tmpP;
            yield return null;
        }
        while(tmpT.a < 1.0f)
        {
            tmpT.a += .5f * Time.deltaTime;
            tmpT.a = Mathf.Min(tmpT.a, 1.0f);
            EWBO.color = tmpT;
            yield return null;
        }
        yield return new WaitForSeconds(4.0f);
        while(tmpT.a > 0.0f)
        {
            tmpT.a -= .5f * Time.deltaTime;
            tmpT.a = Mathf.Max(tmpT.a, 0.0f);
            EWBO.color = tmpT;
            yield return null;
        }
        SceneManager.LoadScene(1);
    }

    private IEnumerator QuietMusic()
    {
        while (Music.volume > 0.0f)
        {
            Music.volume -= .1f * Time.deltaTime;
            Music.volume = Mathf.Max(Music.volume, 0.0f);
            yield return null;
        }
    }
}
