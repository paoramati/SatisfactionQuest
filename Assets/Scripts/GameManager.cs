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
    public bool gameRunning;

    public GameModel gameModel;

    public Dictionary<string, Item> inventory;

    public void AddToInventory(Item pItem)
    {
        inventory.Add(pItem.description, pItem);
    }

    public void DropFromInventory(string pItemName)
    {
        inventory.Remove(pItemName);
    }

    public string InventoryListStr()
    {
        List<String> keyList = new List<string>(inventory.Keys);
        String[] keyArray = keyList.ToArray();
        
        return "Items in the inventory are:\n" + String.Join("\n", keyArray);
    }

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
            GameManager.gameStateInstance = (GameState)bf.Deserialize(file);
            file.Close();
        }
    }
}


public class GameManager : MonoBehaviour {

    //public static GameState instance;
    public static GameManager instance;
    public static GameState gameStateInstance;

    //public GameModel gameModel;

    //private bool gameRunning;

    void Awake() {
		if (instance == null) {
			gameStateInstance = new GameState();
			gameStateInstance.gameRunning = true;
			Debug.Log("I am the one");
            gameStateInstance.gameModel = new GameModel ();
            gameStateInstance.inventory = new Dictionary<string, Item>();
        }
        else {
            Persist.control.Load();
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
}
