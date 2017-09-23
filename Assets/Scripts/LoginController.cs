using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginController : MonoBehaviour {

    private static LoginController instance;

    public string username;
    public bool loginStatus;
    public string password;

    private void Awake()
    {
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
