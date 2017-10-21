﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public string username;
    public string password;
    public bool loginSuccessful;

    InputField inputUsername;
    InputField inputPassword;
    Button btnLogin;
    public Text output;

    InputField.SubmitEvent se;
    Button.ButtonClickedEvent ce;

    void Start()
    {
        output.text = "";
        username = "";
        password = "";

        inputUsername = UnityUtilities.AssignInputField("inputUsername");
        inputPassword = UnityUtilities.AssignInputField("inputPassword");
        btnLogin = UnityUtilities.AssignButton("btnLogin");

        //inputUsername = UnityUtilities.AssignGameObject<InputField>("inputUsername");

        //GameObject usernameObject = GameObject.Find("inputUsername");
        //inputUsername = usernameObject.GetComponent<InputField>();

        //GameObject passwordObject = GameObject.Find("inputPassword");
        //inputPassword = passwordObject.GetComponent<InputField>();

        //btnLogin = GetComponentInChildren<Button>();

        btnLogin.onClick.AddListener(() => CheckLogin());

        //DisplayPlayers();
    }



    //public void AssignButton


    private void CheckLogin()
    {
        output.text = "";
        username = inputUsername.text;
        password = inputPassword.text;

        DataService dataService = new DataService();    //could be replaced by a static object

        dataService.Connect();

        if (inputUsername.text != "" && inputPassword.text != "")       //username and password can not be empty
        {
            if (dataService.CheckUsernameExists(username))              //if this username exists already
            {
                if (dataService.CheckLogin(username, password))         //if login is successful
                {
                    //Debug.Log("Checked Login " + username);

                    //ChangeScene();
                    GameManager.ChangeScene("MenuScene");

                }
                else
                {       
                    //inform user that login was not valid
                    inputUsername.ActivateInputField();
                    output.text = "The username or password is invalid";
                }
            }
            else
            {       
                //add new player
                dataService.AddPlayer(username, password);
                //Debug.Log("Added " + username);
                //ChangeScene();
                GameManager.ChangeScene("MenuScene");

            }
        }
        else
        {
            output.text = "Username or password can not be empty!";
        }
    }

    private void ChangeScene()
    {
        GameManager.ChangeScene("GameScene");
    }

    private void DisplayPlayers()
    {
        DataService dataService = new DataService();    //could be replaced by a static object

        foreach (PlayerDTO player in dataService.GetPlayers())
        {
            Debug.Log(player.Id + " - " + player.Username + " - " + player.Password);
        }
    }


    //public UnityEngine.UI.InputField AssignInputField(string pObjectName)
    //{
    //    InputField lcInputField;
    //    GameObject lcGameObject = GameObject.Find(pObjectName);
    //    return lcInputField = lcGameObject.GetComponent<InputField>();
    //    //return lcInputField;
    //}

    //private void CheckLogin()
    //{
    //    username = inputUsername.text;
    //    password = inputPassword.text;

    //    if (inputUsername.text != "" || inputPassword.text != "")
    //    {

    //        //Tests of player insert and retrieve
    //        DataService dataService = new DataService();

    //        dataService.Connect();

    //        if (dataService.CheckLogin(username, password))     //if login details are valid
    //        {
    //            /*need to: 
    //             * - instantiate a game session using player details
    //             * - load game model according to this player details
    //             * - finally, change to game scene (only thing happening right now)
    //             */


    //            output.text = "Welcome, " + username + "!";

    //            GameManager.ChangeScene("GameScene");
    //        }
    //        else
    //        {
    //            if (dataService.CheckUsername(username))           //if the username is in the database

    //            {
    //                output.text = "Password is incorrect!";
    //            }



    //            else
    //            {
    //                output.text = "Thank you for registering, " + username + "!";

    //                dataService.AddPlayer(username, password);      //adding a player if no player details found that match
    //            }
    //        }




    //        foreach (PlayerDTO player in dataService.GetPlayers())
    //        {
    //            Debug.Log(player.Id + " - " + player.Username + " - " + player.Password);
    //        }
    //        var thisPlayer = dataService.GetPlayer(username);
    //        //Debug.Log(thisPlayer.Username + " - ID = " + thisPlayer.Id);



    //        //output.text = "You entered '" + username + "'";
    //    }
    //    else
    //    {
    //        output.text = "Username and/or password can not be empty";
    //    }
    //}

    // Use this for initialization
    //public void Login()
    //{
    ////GameObject aName = GameObject.Find("IFUserName");
    ////InputField aText = aName.GetComponent<InputField>() ;
    //string name = Name.text;//  aText.text;

    ////GameObject aPassword = GameObject.Find("IFPassword");
    ////aText = aPassword.GetComponent<InputField>();
    //string password = Password.text;// aText.text;

    //DataService aDS = new DataService();
    //if (aDS.CheckLogin(name, password))
    //{
    //    // LOGIN OK
    //}
    //}
}
