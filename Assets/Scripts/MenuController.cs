using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    Button[] btnLogin;
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
    }

    private void NewGame()
    {
        GameManager.instance.CreateNewGameSession();

        GameManager.ChangeScene("GameScene");
    }

    private void LoadGame()
    {
        GameManager.instance.LoadExistingGameSession();

        GameManager.ChangeScene("GameScene");
    }

    private void QuitGame()
    {

    }
}
