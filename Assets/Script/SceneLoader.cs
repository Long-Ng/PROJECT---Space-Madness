using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("B1. Level 1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadLastLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LastLevel"));
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadLoseScene()
    {
        StartCoroutine(DelayLoseScene());
    }
    IEnumerator DelayLoseScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }
}
