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
    public bool _GameRunning;

    public GameModel _GameModel;

    public Dictionary<string, Item> _Inventory;

    public void AddToInventory(Item pItem)
    {
        _Inventory.Add(pItem.description, pItem);
    }

    public void DropFromInventory(string pItemName)
    {
        _Inventory.Remove(pItemName);
    }

    public string InventoryListStr()
    {
        List<String> keyList = new List<string>(_Inventory.Keys);
        String[] keyArray = keyList.ToArray();
        
        return "Items in the inventory are:\n" + String.Join("\n", keyArray);
    }

<<<<<<< HEAD
=======
    //public void setActiveCanvas(string pName)
    //{

    //    if (GameManager.canvases.ContainsKey(pName))
    //    {

    //        // set all to not active;
    //        foreach (Canvas acanvas in GameManager.canvases.Values)
    //        {
    //            acanvas.gameObject.SetActive(false);
    //        }

    //        GameManager.activeCanvas = GameManager.canvases[pName];
    //        Debug.Log("I am the active one " + pName);
    //        GameManager.activeCanvas.gameObject.SetActive(true);

    //    }
    //    else
    //    {
    //        Debug.Log("I can not find " + pName + " to make active.");
    //    }
    //}

    public string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void ChangeScene(string pSceneName)
    {
        SceneManager.LoadScene(pSceneName);
    }

>>>>>>> parent of f2f3a4c... added LoginController
    public bool IsGameRunning()
    {
        return _GameRunning;
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
<<<<<<< HEAD
            GameManager.gameStateInstance = (GameState)bf.Deserialize(file);
=======
            GameManager._Instance = (GameState)bf.Deserialize(file);
>>>>>>> parent of f2f3a4c... added LoginController
            file.Close();
        }
    }
}


public class GameManager : MonoBehaviour {

<<<<<<< HEAD
    //public static GameState instance;
    public static GameManager instance;
    public static GameState gameStateInstance;
=======
	public static GameState _Instance;
>>>>>>> parent of f2f3a4c... added LoginController

    //public GameModel gameModel;

    //private bool gameRunning;

    // What is Awake?
    void Awake() {
<<<<<<< HEAD
		if (instance == null) {
			gameStateInstance = new GameState();
			gameStateInstance.gameRunning = true;
			Debug.Log("I am the one");
            gameStateInstance.gameModel = new GameModel ();
            gameStateInstance.inventory = new Dictionary<string, Item>();
=======
		if (_Instance == null) {
			_Instance = new GameState();
			_Instance._GameRunning = true;
			Debug.Log("I am the one");
            _Instance._GameModel = new GameModel ();
            _Instance._Inventory = new Dictionary<string, Item>();

>>>>>>> parent of f2f3a4c... added LoginController
        }
        else {
            Persist.control.Load();
            Destroy(gameObject);
		}
	}

 
}
