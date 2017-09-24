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
    public Button btnLogin;

    InputField.SubmitEvent se;
    Button.ButtonClickedEvent ce;

    void Start()
    {
        txtUsername = this.GetComponent<InputField>();
        btnLogin = GetComponent<Button>();

        btnLogin.onClick.AddListener(() => CheckLogin());




        //ce = new Button.ButtonClickedEvent();
        //ce.AddListener(CheckLogin);

        //btnLogin.onClick = ce;
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
        //Debug.Log("change material to HIT  on material");

        GameManager.ChangeScene("GameScene");
    }




}
