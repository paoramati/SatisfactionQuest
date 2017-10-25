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

    public GameModel()
    {
        DataService dataService = new DataService();

        worldMap = new Dictionary<string, Location>();
        worldItems = new Dictionary<string, Item>();
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

    //add a pair of exits to the game model
    public void AddWorldLocationExits(string pFromLocation, Location.DIRECTION pDirection, string pDestinationLocation)
    {
        //assign exit to 'from' location
        worldMap[pFromLocation].exits.Add(pDirection, pDestinationLocation);

        //assign reverse exit to 'destination' location
        worldMap[pDestinationLocation].exits.Add(Location.GetOppositeDirection(pDirection), pFromLocation);
    }

    //add an item to the game model
    public void AddWorldItem(string pName, string pLocation, string pSecretLetter)
    {
        worldItems.Add(pName, new Item(pName, pLocation, pSecretLetter));
    }

    //load a location from the db
    public void LoadWorldLocation(LocationDTO pLocationDTO)
    {
        AddWorldLocation(pLocationDTO.Name, pLocationDTO.Description, pLocationDTO.Background);
    }

    //load an exit from the db
    public void LoadWorldLocationExit(ExitDTO pExitDTO)
    {
        Location.DIRECTION lcDirection = (Location.DIRECTION)pExitDTO.Direction;
        AddWorldLocationExits(pExitDTO.FromLocation, lcDirection, pExitDTO.ToLocation);
    }

    //load a session item from the db
    public void LoadWorldItem(ItemDTO pItemDTO)
    {
        AddWorldItem(pItemDTO.Name, pItemDTO.Location, pItemDTO.SecretLetter);
    }

    //update details of an item in the local game model
    public void UpdateWorldItem(ItemDTO pItemDTO)
    {
        worldItems[pItemDTO.Name].id = pItemDTO.Id;
        worldItems[pItemDTO.Name].location = pItemDTO.Location;
    }
}








////load a location to the game model
//public void LoadWorldLocation(int pId, string pLocation, string pDescription, string pBackground)
//{
//    worldMap.Add(pLocation, new Location(pId, pLocation, pDescription, pBackground));
//}

////load an item to the game model
//public void LoadWorldItem(string pName, string pLocation, string pSecretLetter)
//{
//    worldItems.Add(pName, new Item(pName, pLocation, pSecretLetter));
//}

////load a location to the game model
//public void LoadWorldLocation(string pLocation, string pDescription, string pBackground)
//{
//    worldMap[pLocation].
//    //worldMap.Add(pLocation, new Location(pLocation, pDescription, pBackground));
//}
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



//worldMap[pExitDTO.FromLocation].exits.Add(lcDirection, pExitDTO.ToLocation);                                    //assign exit to from location
//worldMap[pExitDTO.ToLocation].exits.Add(Location.GetOppositeDirection(lcDirection), pExitDTO.FromLocation);     //assign reverse exit to destination location
