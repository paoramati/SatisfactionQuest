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

    public int id;
    public string name;
    public string description;
    public string background;
    public List<Item> items;        //deprecated; items have locations instead
    public Dictionary<DIRECTION, string> exits;     //exits only available at given directions. If no match for direction, no exit

    public string question;
    public int answer;

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
        //items = new List<Item>();
        exits = new Dictionary<DIRECTION, string>();
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

        //GameManager.instance.gameModel.worldItems

        DataService dataService = new DataService();

        //var lcItems = dataService.GetLocationItems(name);

        //if (lcItems)

        foreach (ItemDTO item in dataService.GetLocationItems(name))
        {
            lcResult += "- " + item.Name + "\n";
        }

        //if (items.Count == 0)
        //{
        //    lcResult = lcResult + "nothing here.";
        //}
        //else
        //{
        //    foreach (Item item in items)
        //    {
        //        lcResult = lcResult + "\n\t-" + item.name;
        //    }
        //}
        return lcResult;
    }


}

public class Exit
{

}


