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

    InputField txtUsername;
    Button btnLogin;

    InputField.SubmitEvent se;
    Button.ButtonClickedEvent ce;

    private void Start()
    {
        txtUsername = this.GetComponent<InputField>();
        btnLogin = this.GetComponent<Button>();

        ce = new Button.ButtonClickedEvent();
        ce.AddListener(CheckLogin);

        btnLogin.onClick = ce;
        //ce.AddListener


        //GameManager.GetCurrentScene();
    }

    public bool IsLoginValid()
    {

        return false;
    }

    public void LoginPlayer()
    {

    }

    public void CheckLogin()
    {
        GameManager.ChangeScene("GameScene");
    }




}
