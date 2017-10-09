using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Location
{
    //public enum LOCATION_NAME
    //{
    //    tomb, beach, VILLAGE, STALL, CLIFF, HOUSE, CASTLE, FOREST
    //};

    public enum DIRECTION
    {
        NORTH, SOUTH, EAST, WEST
    };

    public int id;
    public string name;
    public string description;
    public string background;
    public List<Item> items;
    public Dictionary<DIRECTION, string> exits;     //exits only available at given directions. If no match for direction, no exit


    public string question;
    public int answer;

    public List<string> locationExits;


    public Location North;
    public Location South;
    public Location East;
    public Location West;
    public Location Previous;

    public Location(string pLocationName)
    {
        name = pLocationName;
        items = new List<Item>();
    }

    public Location(string pName, string pBackground)
    {
        name = pName;
        background = pBackground;
        items = new List<Item>();
        exits = new Dictionary<DIRECTION, string>();
    }

    public Location(string pName, string pDescription, string pBackground)
    {
        name = pName;
        description = pDescription;
        background = pBackground;

        items = new List<Item>();
        exits = new Dictionary<DIRECTION, string>();
    }

    public string GetLocationDetails()
    {
        string lcResult = "Current location: " + name + "\n";
        lcResult += description + "\n";

        if (this.North != null)
            lcResult += "\tTo the North is a " + this.North.name + "\n";
        if (this.South != null)
            lcResult += "\tTo the South is a " + this.South.name + "\n";
        if (this.East != null)
            lcResult += "\tTo the East is a " + this.East.name + "\n";
        if (this.West != null)
            lcResult += "\tTo the West is a " + this.West.name + "\n";

        return lcResult;
    }

    public string GetLocationItems()
    {
        string lcResult = "You can see: ";

        if (items.Count == 0)
        {
            lcResult = lcResult + "nothing here.";
        }
        else
        {
            foreach (Item item in items)
            {
                lcResult = lcResult + "\n\t-" + item.itemName;
            }
        }
        return lcResult;
    }

    
}

public class Exit
{

}


