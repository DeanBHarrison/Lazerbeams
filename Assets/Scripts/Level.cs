using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    public float delayBeforeDeath = 5f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);      
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
         
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayBeforeDeath);
        SceneManager.LoadScene("Game Over");
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
