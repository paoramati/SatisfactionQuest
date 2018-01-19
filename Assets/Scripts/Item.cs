using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string name;
    public string location;
    public int sessionId;
    public string description;
    public string secretLetter;

    public Item(string pName, string pLocation, string pSecretLetter)
    {
        name = pName;
        location = pLocation;
        secretLetter = pSecretLetter;
        sessionId = GameManager.instance.Id;        //get item's session from game manager
    }

    public string GetLocationItems()
    {
        string lcResult = "You can see: \n";

        foreach (var item in GameManager.instance.gameModel.worldItems)
        {
            if (item.Value.location == name)
            {
                lcResult += "- " + item.Value.name + "\n";      //output name of item
            }
        }

        return lcResult;
    }
}