using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour {

    //private static LoginController instance;

    public string username;
    public bool loginStatus;
    public string password;
    public InputField txtUsername;
    public Button btnLogin;

    private void Awake()
    {
        txtUsername = this.GetComponent<InputField>();
        btnLogin = this.GetComponent<Button>();
        GameManager.GetCurrentScene();
    }

    public bool IsLoginValid()
    {

        return false;
    }

    public void LoginPlayer()
    {

    }


	

}
