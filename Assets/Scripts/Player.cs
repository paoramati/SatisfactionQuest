using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player {

    public int playerID;
    public string playerName;
    public int ep;              //esteem points
    public int score;
    public List<Item> inventory;

    public Player()
    {

    }
}
