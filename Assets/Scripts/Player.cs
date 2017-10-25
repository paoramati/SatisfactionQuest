using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

[Serializable]
public class Player {

    public int playerId;
    public string username;
    public int esteem;              //esteem points
    public int score;

    public Player()
    {

    }

    public Player(string pUsername)
    {
        username = pUsername;
    }

    internal string GetInventoryItems()
    {
        string lcResult = "You have: \n";

        foreach (var item in GameManager.instance.gameModel.worldItems)
        {
            if (item.Value.location == username)
            {
                lcResult += "- " + item.Value.name + "\n";
            }
        }

        return lcResult;
    }

    //internal string GetInventoryItems()
    //{
    //    string lcResult = "You have: \n";

    //    DataService dataService = new DataService();

    //    foreach (ItemDTO item in dataService.GetPlayerItems(GameManager.instance.sessionId, username))
    //    {
    //        lcResult += "- " + item.Name + "\n";
    //    }

    //    return lcResult;

    //}
}
