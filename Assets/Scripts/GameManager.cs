using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class GameState
{
    public GameModel gameModel;
    public bool gameRunning;

    public int sessionId;

    public Player player1;
    public Player player2;

    public Dictionary<string, Item> inventory;

    public GameState()
    {
        gameRunning = false;
    }

    public GameState(string pUsername)
    {
        player1 = new Player(pUsername);
        gameRunning = false;
    }

    public bool IsGameRunning()
    {
        return gameRunning;
    }

    internal void CreateNewGameSession()
    {
        DataService dataService = new DataService();

        sessionId = dataService.CreateGameSession(player1.username);    //create session
        GameManager.instance.gameModel = new GameModel();               //create gameModel
        //GameManager.instance.gameModel.GenerateWorldItems();            //generate world items
        //dataService.SaveSessionItems();
        dataService.CreateSessionItems(sessionId);                      //save session items to database
        dataService.UpdateLocalItems();
        //dataService.SaveSessionItems();
        //dataService

    }

    internal void LoadExistingGameSession()
    {
        DataService dataService = new DataService();

        //Debug.Log("LoadExistingGameSession() ");

        if (dataService.PreviousSessionExists(player1.username))
        {

            var previousSession = dataService.GetPreviousSession(player1.username);
            sessionId = previousSession.Id;

            //Debug.Log("Previous Game Exists - Session Id =  " + sessionId);

            player1.esteem = previousSession.Esteem_Player1;
            GameManager.instance.gameModel = new GameModel();
            dataService.UpdateLocalItems();

            //GameManager.instance.gameModel.worldItems.Clear();

            //dataService.LoadSessionItems();
        }
        else
        {
            Debug.Log("No Previous Game Exists");
            //CreateNewGameSession();
        }
    }

    public void SaveGameState()
    {
        DataService dataService = new DataService();

        //dataService.CreateSessionItems(sessionId);        //need to fetch session id from game manager?
    }

    public void LoadGameState()
    {
        DataService dataService = new DataService();
    }
}


public class GameManager : MonoBehaviour {

    public static GameState instance;

    public GameModel gameModel;


    // What is Awake?
    void Awake() {

		if (instance == null) {

			Debug.Log("I am the one");

        }
        else {
            Destroy(gameObject);
		}	
	}

    public static void InitializeGameState(string pUsername)
    {
        instance = new GameState(pUsername);
    }

    public static string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public static void ChangeScene(string pSceneName)
    {
        SceneManager.LoadScene(pSceneName);
    }

}


