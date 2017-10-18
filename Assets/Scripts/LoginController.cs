using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public string username;
    public string password;
    public bool loginStatus;

    InputField inputUsername;
    InputField inputPassword;
    Button btnLogin;
    public Text output;

    InputField.SubmitEvent se;
    Button.ButtonClickedEvent ce;

    void Start()
    {
        output.text = "";

        GameObject usernameObject = GameObject.Find("inputUsername");
        inputUsername = usernameObject.GetComponent<InputField>();

        GameObject passwordObject = GameObject.Find("inputPassword");
        inputPassword = passwordObject.GetComponent<InputField>();

        btnLogin = GetComponentInChildren<Button>();



        //if (inputLogin != null)
        {
            Debug.Log("in if");
            username = "";
            password = "";

            btnLogin.onClick.AddListener(() => CheckLogin());
        }
    }

    private void CheckLogin()
    {
        username = inputUsername.text;
        password = inputPassword.text;

        if (inputUsername.text != "" && inputPassword.text != "")
        {

            //Tests of player insert and retrieve
            DataService dataService = new DataService();
            
            dataService.Connect();

            if (dataService.CheckLogin(username, password))     //if login details are valid
            {
                /*need to: 
                 * - instantiate a game session using player details
                 * - load game model according to this player details
                 * - finally, change to game scene (only thing happening right now)
                 */ 
                  
                


                //GameManager.ChangeScene("GameScene");
            }
            else
            {
                dataService.AddPlayer(username, password);      //adding a player if no player details found that match
            }

            


            foreach (PlayerDTO player in dataService.GetPlayers())
            {
                Debug.Log(player.Id + " - " + player.Username + " - " + player.Password);
            }
            var thisPlayer = dataService.GetPlayer(username);
            //Debug.Log(thisPlayer.Username + " - ID = " + thisPlayer.Id);



            output.text = "You entered '" + username + "'";
        }
        else
        {
            output.text = "Username and/or password can not be empty";
        }
    }


    // Use this for initialization
    public void Login()
    {
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
    }

}
