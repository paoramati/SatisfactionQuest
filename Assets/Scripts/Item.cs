using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string itemName;
    public string description;
    public string secretLetter;
    public string SecretQuestion;
    public bool itemFound = false;
    public Item nextItem;

    public Item(string pName, string pSecretLetter)
    {
        itemName = pName;
        secretLetter = pSecretLetter;
    }

    public Item(string pName)
    {
        itemName = pName;
    }
}