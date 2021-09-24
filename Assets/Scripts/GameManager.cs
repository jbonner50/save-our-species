using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static bool gameOver = false;
    public static bool offScreen = false;

    private void Start()
    {
        gameOver = false;
    }

    private void Update()
    {
        if(gameOver)
        {
            StartCoroutine(GameOver());
        }
    }
    public static void PlayGame()
    {
        gameOver = false;
        SceneManager.LoadScene(1);

    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void MainMenu()
    {
        SceneManager.LoadScene(0);
        
    }


    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        MainMenu();
    }

    public static void ResetAspectRatio()
    {
        Screen.SetResolution(1920, 1080, false);
    }
}
