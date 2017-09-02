using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public GameModel gameModel;

    private bool gameRunning;

    // What is Awake?
    void Awake() {
		if (instance == null) {
			instance = this;
			gameRunning = true;
			Debug.Log("I am the one");
            gameModel = new GameModel ();
		} else {
            Destroy(gameObject);
		}
	
	}

    public string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void ChangeScene(string pSceneName)
    {
        SceneManager.LoadScene(pSceneName);
    }

    public bool IsGameRunning(){
		return gameRunning;
	}
}
