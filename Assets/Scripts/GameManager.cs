﻿using System.Collections;
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
    public bool gameRunning;

    public int sessionId;

    //public 

    public GameModel gameModel;

    public Dictionary<string, Item> inventory;

    public GameState()
    {
        gameRunning = false;
    }

    //public void AddToInventory(Item pItem)
    //{
    //    inventory.Add(pItem.description, pItem);
    //}
    //public void DropFromInventory(string pItemName)
    //{
    //    inventory.Remove(pItemName);
    //}

    //public string InventoryListStr()
    //{
    //    List<String> keyList = new List<string>(inventory.Keys);
    //    String[] keyArray = keyList.ToArray();
    //    // 
    //    return "Items in the inventory are:\n" + String.Join("\n", keyArray);
    //}

    //public string GetCurrentScene()
    //{
    //    return SceneManager.GetActiveScene().name;
    //}

    //public void ChangeScene(string pSceneName)
    //{
    //    SceneManager.LoadScene(pSceneName);
    //}

    public bool IsGameRunning()
    {
        return gameRunning;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/models.dat");
        bf.Serialize(file, this);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/models.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/models.dat", FileMode.Open);
            GameManager.instance = (GameState)bf.Deserialize(file);
            file.Close();

        }

    }
}



public class GameManager : MonoBehaviour {

    //public static GameState instance;
    public static GameState instance;
    //public GameState gameState;

	//public static GameState instance;

    // What is Awake?
    void Awake() {

		if (instance == null) {
			instance = new GameState();
			instance.gameRunning = true;
			Debug.Log("I am the one");
            instance.gameModel = new GameModel();

            /*
             * FROM MAIN MENU BUTTON
             * if new game, then the game model should be newly created
             * if load game, then the game model should be loaded form saved game state
             * 
             * if (new game)
             * {
             *      sessionId = some incremental token
             *      OR create a new session (GameStateDTO), then
             *      use it's id here to set the sessionId value
             *      instance.gameModel.Load 
             * 
             * 
             * 
             */




            instance.gameModel.SaveGameState();
            //instance.inventory = new Dictionary<string, Item>();
        }
        else {
            Persist.control.Load();
            Destroy(gameObject);
		}	
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
