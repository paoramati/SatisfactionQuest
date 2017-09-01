using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	public static GameManager instance;

	private bool gameRunning;

	public GameModel gameModel;


	public string currentUScene()
	{
		return SceneManager.GetActiveScene ().name;
	}

	public void changeUScene(string pSceneName){
		SceneManager.LoadScene (pSceneName);
	}	



	// What is Awake?
	// What other handlers are there?
	void Awake() {
		if (instance == null) {
			instance = this;
			gameRunning = true;
			Debug.Log("I am the one");
            gameModel = new GameModel ();
		} else {
            Debug.Log("Destroying a GameManager");

            Destroy(gameObject);
		}
	
	}
	
	public bool IsGameRunning(){
		return gameRunning;
	}
}
