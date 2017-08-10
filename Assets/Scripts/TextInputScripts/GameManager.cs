using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	private bool gameRunning;

	public GameModel gameModel;

	// What is Awake?
	// What other handlers are there?
	void Awake() {
		if (instance == null) {
			instance = this;
			gameRunning = true;
			Debug.Log("I am the one");
			gameModel = new GameModel ();
		} else {
			Destroy (gameObject);
		}
	
	}
	
	public bool IsGameRunning(){
		return gameRunning;
	}
}
