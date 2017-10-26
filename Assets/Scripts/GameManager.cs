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
    private int id;
    private Player player1;
    private Player player2;

    public bool gameRunning;

    public Player Player1
    {
        get
        {
            return player1;
        }

        set
        {
            player1 = value;
        }
    }

    public Player Player2
    {
        get
        {
            return player2;
        }

        set
        {
            player2 = value;
        }
    }

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public GameState()
    {
        gameRunning = false;
    }

    public GameState(string pUsername)
    {
        Player1 = new Player(pUsername);
        gameRunning = false;
    }

    public bool IsGameRunning()
    {
        return gameRunning;
    }

    //create new game session
    internal void CreateNewGameSession()
    {
        DataService dataService = new DataService();

        Id = dataService.CreateGameSession(Player1.username);    //create session
        GameManager.instance.gameModel = new GameModel();               //create gameModel
        GameManager.instance.gameModel.GenerateWorldMap();
        GameManager.instance.gameModel.GenerateWorldItems();

        dataService.CreateSessionItems(Id);                      //save session items to database
    }

    /**
     * Load previous game session of the player.
     * Creates new game session if none exists
     */
    internal void LoadExistingGameSession()
    {
        DataService dataService = new DataService();

        if (dataService.PreviousSessionExists(Player1.username))
        {
            var previousSession = dataService.GetPreviousSession(Player1.username);     //previous sessionDTO   
            Id = previousSession.Id;                                                    
            Player1.esteem = previousSession.Esteem_Player1;                            
            GameManager.instance.gameModel = new GameModel();
            GameManager.instance.gameModel.GenerateWorldMap();
            GameManager.instance.gameModel.currentLocation = GameManager.instance.gameModel.worldMap[previousSession.Location_Player1];
            GameManager.instance.gameModel.firstLocation = GameManager.instance.gameModel.currentLocation;                                  //set first location
            dataService.LoadSessionItems();
        }
        else
        {
            Debug.Log("No Previous Game Exists");       //no attractive option for no previous games
            CreateNewGameSession();         
        }
    }

    //save current game state (items, player location, esteem)
    public void SaveGameState()
    {
        DataService dataService = new DataService();

        dataService.SaveSessionItems();

        //dataService.CreateSessionItems(sessionId);        //need to fetch session id from game manager?
    }

    //load last saved game state
    public void LoadGameState()
    {
        DataService dataService = new DataService();
    }
}


public class GameManager : MonoBehaviour
{

    public static GameState instance;

    public GameModel gameModel;

    public Text lblEsteem;


    // What is Awake?
    void Awake()
    {

        if (instance == null)
        {

            Debug.Log("I am the one");

        }
        else
        {
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


