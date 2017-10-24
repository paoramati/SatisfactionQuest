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
}
