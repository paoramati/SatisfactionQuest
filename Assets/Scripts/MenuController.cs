using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    Button[] btnLogin;
    List<Button> buttons;
    Dictionary<string, Button> buttonsDictionary;

    // Use this for initialization
    void Start()
    {
        buttonsDictionary = new Dictionary<string, Button>();

        buttonsDictionary.Add("New", UnityUtilities.AssignButton("btnNewGame"));
        buttonsDictionary.Add("Load", UnityUtilities.AssignButton("btnLoadGame"));
        buttonsDictionary.Add("Quit", UnityUtilities.AssignButton("btnQuit"));

        buttonsDictionary["New"].onClick.AddListener(() => NewGame());
        buttonsDictionary["Load"].onClick.AddListener(() => LoadGame());
        buttonsDictionary["Quit"].onClick.AddListener(() => QuitGame());


        //GameObject btnNew = GameObject.Find("btnNewGame");
        //buttons.Add("New", btnNew.GetComponent<Button>());

        //GameObject btnLoad = GameObject.Find("btnLoadGame");
        //buttons.Add("Load", btnLoad.GetComponent<Button>());

        //GameObject btnQuit = GameObject.Find("btnQuit");
        //buttons.Add("Quit", btnQuit.GetComponent<Button>());

        //buttons["New"].onClick.AddListener(() => NewGame());
        //buttons["Load"].onClick.AddListener(() => LoadGame());
        //buttons["Quit"].onClick.AddListener(() => QuitGame());

        //buttons = new List<Button>();





    }

    private void NewGame()
    {
        Debug.Log("NewGame()");
        GameManager.ChangeScene("GameScene");
    }

    private void LoadGame()
    {
        Debug.Log("LoadGame()");
        GameManager.ChangeScene("GameScene");
    }

    private void QuitGame()
    {

    }
}
