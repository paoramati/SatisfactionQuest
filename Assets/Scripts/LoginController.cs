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

    public InputField inputLogin;
    InputField input;
    Button btnLogin;

    InputField.SubmitEvent se;
    Button.ButtonClickedEvent ce;

    void Start()
    {
        //inputLogin = GetComponent<InputField>();
        input = GetComponentInChildren<InputField>();


        btnLogin = GetComponentInChildren<Button>();

        //if (inputLogin != null)
        {
            Debug.Log("in if");
            username = "";


            //se = new InputField.SubmitEvent();
            //se.AddListener(SetUsername);
            //input.onEndEdit = se;


            btnLogin.onClick.AddListener(() => CheckLogin());

        }
    }

    private void CheckLogin()
    {

        SetUsername(input.text);

        if (input.text != "")
        {
            Debug.Log("input = " + input.text);
            //GameManager.ChangeScene("GameScene");
        }
        else
        {
            Debug.Log("No username");
        }
    }

    private void SetUsername(string arg0)
    {
        username = input.text;
        Debug.Log("username = " + username);
        //inputLogin.ActivateInputField();

    }

    private void CheckLogin(string arg0)
    {
        Debug.Log("not working");
    }

}
