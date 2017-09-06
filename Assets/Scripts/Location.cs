using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location {

    public string locationName;
    public string backgroundImageName;
    public Item item;
    public string question;
    public int answer;

    public Location North;
    public Location South;
    public Location East;
    public Location West;
    public Location Previous;

    public Location(string pLocationName)
    {
        locationName = pLocationName;
    }

    public Location(string pLocationName, string pImageName)
	{
        locationName = pLocationName;
        backgroundImageName = pImageName;
	}

    public string GetLocationDetails()
    {
        string lcResult = "You are at a " + locationName + ":\n";

        if (this.North != null)
            lcResult += "\tTo the North is a " + this.North.locationName + "\n";
        if (this.South != null)
            lcResult += "\tTo the South is a " + this.South.locationName + "\n";
        if (this.East != null)
            lcResult += "\tTo the East is a " + this.East.locationName + "\n";
        if (this.West != null)
            lcResult += "\tTo the West is a " + this.West.locationName + "\n";

        return lcResult;
    }
    
    public string GetLocationItems()
    {
        string lcResult = "You can see: ";
        Item currentItem = item;
        if (currentItem == null)
        {
            lcResult = lcResult + "nothing here.";
        }
        else
        {
            while (currentItem != null)
            {
                lcResult = lcResult + "\n" + currentItem.itemName;
                currentItem = currentItem.nextItem;
            }
        }
        return lcResult;
    }
}

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
