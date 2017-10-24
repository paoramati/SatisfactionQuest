using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string name;
    public string location;
    public string description;
    public string secretLetter;
    public string SecretQuestion;
    public bool itemFound = false;
    public Item nextItem;

    public enum NAME
    {
        JAR,
        MOP,
        SHELL,
        TOE,
        HELMET,
        CUP,
        COMPUTER,
        FIREPLACE
    }


    public Item(string pName, string pLocation, string pSecretLetter)
    {
        name = pName;
        location = pLocation;
        secretLetter = pSecretLetter;
    }

    public Item(NAME pId, string pName, string pLocation, string pSecretLetter)
    {
        id = (int)pId;
        //name = pName;
        //location = pLocation;
        name = pName.ToLower();
        location = pLocation.ToLower();
        secretLetter = pSecretLetter;
    }

    public Item(string pName, string pLocation)
    {
        name = pName;
        location = pLocation;
    }

    //public Item(string pName, string pSecretLetter)
    //{
    //    name = pName;
    //    secretLetter = pSecretLetter;
    //}

    public Item(string pName)
    {
        name = pName;
    }
}