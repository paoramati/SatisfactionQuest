using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour {

    private static LoginController instance;

    public string username;
    public bool loginStatus;
    public string password;
    InputField txtUsername;
    //Bu

    private void Awake()
    {
        txtUsername = this.GetComponent<InputField>();
    }

    public void CheckLogin()
    {
        username = "placeholderName";       //placeholder initialisation. REMOVE WHEN DB ACCESS ACHIEVED!


        if (IsLoginValid())
        {
            LoginPlayer();
        }
        else
        {

        }
    }

    public bool IsLoginValid()
    {
        bool lcIsValid = false;
        if (txtUsername.text == username)
        {
            lcIsValid = true;
        }
        return lcIsValid;
    }

    public void LoginPlayer()
    {
        //GameManager.instance
    }


	

}
