using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string name;
    public string location;
    public int itemId;
    public int sessionId;
    public string description;
    public string secretLetter;
    public string SecretQuestion;
    public bool itemFound = false;
    public Item nextItem;

    public enum NAME
    {
        JAR = 1,
        MOP,
        SHELL,
        TOE,
        HELMET,
        CUP,
        COMPUTER,
        FIREPLACE
    }


    //public Item(NAME pId, string pName, string pLocation, string pSecretLetter)
    //{
    //    id = (int)pId;
    //    name = pName;
    //    location = pLocation;
    //    secretLetter = pSecretLetter;
    //    sessionId = GameManager.instance.sessionId;
    //}

    public Item(string pName, string pLocation, string pSecretLetter)
    {
        name = pName;
        location = pLocation;
        secretLetter = pSecretLetter;
        sessionId = GameManager.instance.sessionId;
    }

    //public Item(int pId, string pName, string pLocation, string pSecretLetter)
    //{
    //    id = pId;
    //    name = pName;
    //    location = pLocation;
    //    secretLetter = pSecretLetter;
    //}

    //public Item(NAME pId, string pName, string pLocation, string pSecretLetter, int pSessionId)
    //{
    //    id = (int)pId;
    //    name = pName;
    //    location = pLocation;
    //    secretLetter = pSecretLetter;
    //    sessionId = pSessionId;
    //}

    public void ChangeItemLocation(string pLocationOrUsername)
    {
        location = pLocationOrUsername;
    }

    public void SetSessionItemId(int pSessionItemId)
    {

    }
}