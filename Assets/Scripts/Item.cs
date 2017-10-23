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

    public Item(string pName, string pLocation, string pSecretLetter)
    {
        name = pName;
        location = pLocation;
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