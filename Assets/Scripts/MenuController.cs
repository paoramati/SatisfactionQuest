using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    Button[] btnLogin;
    //List<Button> buttons;
    Dictionary<string, Button> buttons;

    // Use this for initialization
    void Start()
    {
        buttons = new Dictionary<string, Button>();

        GameObject btnNew = GameObject.Find("btnNewGame");
        buttons.Add("New", btnNew.GetComponent<Button>());

        GameObject btnLoad = GameObject.Find("btnLoadGame");
        buttons.Add("Load", btnLoad.GetComponent<Button>());

        GameObject btnQuit = GameObject.Find("btnQuit");
        buttons.Add("Quit", btnQuit.GetComponent<Button>());

        buttons["New"].onClick.AddListener(() => NewGame());
        buttons["Load"].onClick.AddListener(() => LoadGame());
        buttons["Quit"].onClick.AddListener(() => QuitGame());
    }

    private void NewGame()
    {

    }

    private void LoadGame()
    {

    }

    private void QuitGame()
    {

    }
}
