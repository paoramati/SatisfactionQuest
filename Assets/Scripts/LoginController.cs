using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{

    //private static LoginController instance;

    public string username;
    public bool loginStatus;
    public string password;

    InputField input;
    Button btnLogin;
    public Text output;

    InputField.SubmitEvent se;
    Button.ButtonClickedEvent ce;

    void Start()
    {
        output.text = "";
        input = GetComponentInChildren<InputField>();
        btnLogin = GetComponentInChildren<Button>();

        //if (inputLogin != null)
        {
            Debug.Log("in if");
            username = "";

            btnLogin.onClick.AddListener(() => CheckLogin());
        }
    }

    private void CheckLogin()
    {
        username = input.text;

        if (input.text != "")
        {
            Debug.Log("input = " + input.text);
            output.text = "You entered '" + username + "'"; 
        }
        else
        {
            Debug.Log("No username");
            output.text = "Username can not be empty";

        }
    }

}
