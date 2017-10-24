using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Location
{
    public enum DIRECTION
    {
        NORTH, SOUTH, EAST, WEST
    };

    public enum NAME
    {
        TOMB = 1,
        BEACH,
        VILLAGE,
        STALL,
        CLIFF,
        HOUSE,
        CASTLE,
        FOREST,
        DESERT,
        PLAIN,
        SWAMP,
        ESTUARY,
        PADDOCK,
        MARSH
    }

    public int id;
    public string name;
    public string description;
    public string background;
    public Dictionary<DIRECTION, NAME> exits;     //exits only available at given directions. If no match for direction, no exit


    public List<Item> items;                        //deprecated; items have locations instead
    
    //public Dictionary<DIRECTION, string> exits;     //exits only available at given directions. If no match for direction, no exit


    public string question;
    public int answer;

    //public Location(string pLocationName)
    //{
    //    name = pLocationName;
    //    items = new List<Item>();
    //}

    //public Location(string pName, string pBackground)
    //{
    //    name = pName;
    //    background = pBackground;
    //    items = new List<Item>();
    //    exits = new Dictionary<DIRECTION, string>();
    //}

    //public Location(string pName, string pDescription, string pBackground)
    //{
    //    //id = 
    //    name = pName;
    //    description = pDescription;
    //    background = pBackground;
    //    //items = new List<Item>();
    //    exits = new Dictionary<DIRECTION, string>();
    //}

    public Location(int pId, string pName, string pDescription, string pBackground)
    {
        id = pId;
        name = pName;
        description = pDescription;
        background = pBackground;
        exits = new Dictionary<DIRECTION, NAME>();
    }

    public Location(NAME pId, string pName, string pDescription, string pBackground)
    {
        id = (int)pId;
        name = pName;
        description = pDescription;
        background = pBackground;
        exits = new Dictionary<DIRECTION, NAME>();
    }

    public string GetLocationDetails()
    {
        string lcResult = "Current location: " + name + "\n" + description + "\n";
        //lcResult += GetLocationExits();
        return lcResult;
    }

    public string GetLocationExits()
    {
        string lcResult = "";

        if (exits.ContainsKey(DIRECTION.NORTH))
            lcResult += "\tTo the North is a " + exits[DIRECTION.NORTH] + "\n";
        if (exits.ContainsKey(DIRECTION.SOUTH))
            lcResult += "\tTo the South is a " + exits[DIRECTION.SOUTH] + "\n";
        if (exits.ContainsKey(DIRECTION.EAST))
            lcResult += "\tTo the East is a " + exits[DIRECTION.EAST] + "\n";
        if (exits.ContainsKey(DIRECTION.WEST))
            lcResult += "\tTo the West is a " + exits[DIRECTION.WEST] + "\n";

        return lcResult;
    }

    public string GetLocationItems()
    {
        string lcResult = "You can see: \n";

        foreach (var item in GameManager.instance.gameModel.worldItems)
        {
            if (item.Value.location == name)
            {
                lcResult += "- " + item.Value.name + "\n";
            }
        }

        //DataService dataService = new DataService();

        //foreach (ItemDTO item in dataService.GetSessionLocationItems(GameManager.instance.sessionId, name))
        //{
        //    lcResult += "- " + item.Name + "\n";
        //}

        return lcResult;
    }

    public static DIRECTION GetOppositeDirection(DIRECTION pDirection)
    {
        DIRECTION oppositeDirection = DIRECTION.NORTH;  //initialize with north

        if (pDirection == DIRECTION.NORTH)
            oppositeDirection = DIRECTION.SOUTH;
        if (pDirection == DIRECTION.SOUTH)
            oppositeDirection = DIRECTION.NORTH;
        if (pDirection == DIRECTION.EAST)
            oppositeDirection = DIRECTION.WEST;
        if (pDirection == DIRECTION.WEST)
            oppositeDirection = DIRECTION.EAST;

        return oppositeDirection;
    }


}

public class Exit
{

}


