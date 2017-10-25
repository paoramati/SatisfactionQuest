using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public Dictionary<string, Location> worldMap;
    public Dictionary<string, Item> worldItems;
    public Location firstLocation;
    public Location currentLocation;

    private DataService dataService;

    public GameModel()
    {
        worldMap = new Dictionary<string, Location>();
        worldItems = new Dictionary<string, Item>();

        GenerateWorldMap();
        GenerateWorldItems();
    }

    public void GenerateWorldMap()
    {
        //add locations
        AddWorldLocation("tomb", "A smelly tomb with a smelly corpse", "sign3");
        AddWorldLocation("beach", "A pretty beach with beautiful nothing to see.", "beach1");
        AddWorldLocation("village", "A stupid village with stupid people.", "village1");
        AddWorldLocation("stall", "An uncomfortable kissing booth.", "booth1");
        AddWorldLocation("house", "A stupid interior of a stupid house.", "house1");
        AddWorldLocation("forest", "A dark, dark, musty forest.", "forest1");
        AddWorldLocation("plain", "A perfectly unremarkable plain", "grass1");

        //add location exits - also adds opposite exit from other location
        AddWorldLocationExits("tomb", Location.DIRECTION.NORTH, "village");
        AddWorldLocationExits("tomb", Location.DIRECTION.WEST, "forest");
        AddWorldLocationExits("tomb", Location.DIRECTION.SOUTH, "stall");
        AddWorldLocationExits("tomb", Location.DIRECTION.EAST, "plain");

        AddWorldLocationExits("village", Location.DIRECTION.EAST, "beach");
        AddWorldLocationExits("village", Location.DIRECTION.NORTH, "house");

        AddWorldLocationExits("plain", Location.DIRECTION.NORTH, "beach");

        currentLocation = worldMap["tomb"];

        firstLocation = currentLocation;        
    }


    public void GenerateWorldItems()
    {
        AddWorldItem("jar", "tomb", "i");
        AddWorldItem("mop", "tomb", "j");
        AddWorldItem("computer", "house", "c");
        AddWorldItem("cup", "stall", "a");
        AddWorldItem("fireplace", "house", "g");
        AddWorldItem("helmut", "plain", "o");
        AddWorldItem("shell", "beach", "p");
        AddWorldItem("toe", "village", "r");

    }

    //add a location to the game model
    public void AddWorldLocation(string pLocation, string pDescription, string pBackground)
    {
        worldMap.Add(pLocation, new Location(pLocation, pDescription, pBackground));
    }

    //add an exit to the game model
    public void AddWorldLocationExits(string pFromLocation, Location.DIRECTION pDirection, string pDestinationLocation)
    {
        //assign exit to from location
        worldMap[pFromLocation].exits.Add(pDirection, pDestinationLocation);
        
        //assign reverse exit to destination location
        worldMap[pDestinationLocation].exits.Add(Location.GetOppositeDirection(pDirection), pFromLocation);    
    }

    //add an item to the game model
    public void AddWorldItem(string pName, string pLocation, string pSecretLetter)
    {
        worldItems.Add(pName, new Item(pName, pLocation, pSecretLetter));
    }

    //load a location to the game model
    public void LoadWorldLocation(string pLocation, string pDescription, string pBackground)
    {
        worldMap.Add(pLocation, new Location(pLocation, pDescription, pBackground));
    }

    //load a location to the game model
    public void LoadWorldLocation(int pId, string pLocation, string pDescription, string pBackground)
    {
        worldMap.Add(pLocation, new Location(pId, pLocation, pDescription, pBackground));
    }

    //load an exit to the game model
    public void LoadWorldLocationExits(string pFromLocation, int pDirection, string pDestinationLocation)
    {
        Location.DIRECTION lcDirection = (Location.DIRECTION)pDirection;

        worldMap[pFromLocation].exits.Add(lcDirection, pDestinationLocation);                                    //assign exit to from location
        worldMap[pDestinationLocation].exits.Add(Location.GetOppositeDirection(lcDirection), pFromLocation);     //assign reverse exit to destination location
    }

    //load an item to the game model
    public void LoadWorldItem(string pName, string pLocation, string pSecretLetter)
    {
        //Debug.Log("pName = " + pName + " worldItems[pName] = " + worldItems.Keys);
        worldItems.Add(pName, new Item(pName, pLocation, pSecretLetter));



    }

    public void LoadWorldItem(ItemDTO pItemDTO)
    {
        worldItems[pItemDTO.Name].id = pItemDTO.Id;
        worldItems[pItemDTO.Name].location = pItemDTO.Location;

    }
}



//public static List<string> locationNames = new List<string>
//    {
//        "tomb",         //0
//        "beach",        //1
//        "village",      //2
//        "stall",        //3
//        "cliff",        //4
//        "house",        //5
//        "castle",       //6
//        "forest",       //7
//        "desert",       //8
//        "plain",        //9
//        "swamp",        //10
//        "estuary",      //11
//        "paddock",      //12
//        "marsh"         //13
//    };
