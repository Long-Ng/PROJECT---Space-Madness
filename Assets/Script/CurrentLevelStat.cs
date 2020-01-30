using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentLevelStat : MonoBehaviour {

    [SerializeField] Player player;

	// Use this for initialization
	void Start ()
    {
        PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
	}

    public string GetLastLevel()
    {
        return PlayerPrefs.GetString("LastLevel");
    }

    void Update()
    {
        if (player.GetPlayerHealth() <= 0)
        {
            StartCoroutine(LoadGameOverScene());
           
        }
    }
	
    IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }
}
