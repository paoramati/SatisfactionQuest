using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    //public Dictionary<string, Location> locationMap;
    public Dictionary<string, Location> worldMap;
    public Dictionary<string, Item> worldItems;
    //public Dictionary<Location.NAME, Location> worldMap;
    //public Dictionary<Item.NAME, Item> worldItems;

    //public List<Item> worldItems;
    public Location firstLocation;
    public Location currentLocation;



    private DataService dataService;

    public GameModel()
    {
        GenerateWorldMap();
        GenerateWorldItems();
    }

    public void GenerateWorldMap()
    {
        //worldMap = new Dictionary<Location.NAME, Location>();
        worldMap = new Dictionary<string, Location>();

        //add locations
        AddWorldLocation(Location.NAME.TOMB, "A smelly tomb with a smelly corpse", "sign3");
        AddWorldLocation(Location.NAME.BEACH, "A pretty beach with beautiful nothing to see.", "beach1");
        AddWorldLocation(Location.NAME.VILLAGE, "A stupid village with stupid people.", "village1");
        AddWorldLocation(Location.NAME.STALL, "An uncomfortable kissing booth.", "booth1");
        AddWorldLocation(Location.NAME.HOUSE, "A stupid interior of a stupid house.", "house1");
        AddWorldLocation(Location.NAME.FOREST, "A dark, dark, musty forest.", "forest1");
        AddWorldLocation(Location.NAME.PLAIN, "A perfectly unremarkable plain", "grass1");

        //add location exits - also adds opposite exit from other location
        AddWorldLocationExits(Location.NAME.TOMB, Location.DIRECTION.NORTH, Location.NAME.VILLAGE);
        AddWorldLocationExits(Location.NAME.TOMB, Location.DIRECTION.WEST, Location.NAME.FOREST);
        AddWorldLocationExits(Location.NAME.TOMB, Location.DIRECTION.SOUTH, Location.NAME.STALL);
        AddWorldLocationExits(Location.NAME.TOMB, Location.DIRECTION.EAST, Location.NAME.PLAIN);

        AddWorldLocationExits(Location.NAME.VILLAGE, Location.DIRECTION.EAST, Location.NAME.BEACH);
        AddWorldLocationExits(Location.NAME.VILLAGE, Location.DIRECTION.NORTH, Location.NAME.HOUSE);

        AddWorldLocationExits(Location.NAME.PLAIN, Location.DIRECTION.NORTH, Location.NAME.BEACH);

        currentLocation = worldMap[Location.NAME.TOMB.ToString().ToLower()];

        firstLocation = currentLocation;        
    }


    public void GenerateWorldItems()
    {
        worldItems = new Dictionary<string, Item>();

        AddWorldItem(Item.NAME.JAR, Location.NAME.TOMB, "i");
        AddWorldItem(Item.NAME.MOP, Location.NAME.TOMB, "j");
        AddWorldItem(Item.NAME.COMPUTER, Location.NAME.HOUSE, "c");
        AddWorldItem(Item.NAME.CUP, Location.NAME.STALL, "a");
        AddWorldItem(Item.NAME.FIREPLACE, Location.NAME.HOUSE, "g");
        AddWorldItem(Item.NAME.HELMET, Location.NAME.PLAIN, "o");
        AddWorldItem(Item.NAME.SHELL, Location.NAME.BEACH, "p");
        AddWorldItem(Item.NAME.TOE, Location.NAME.VILLAGE, "r");

    }

    //add a location to the game model
    private void AddWorldLocation(Location.NAME pLocation, string pDescription, string pBackground)
    {

        worldMap.Add(pLocation.ToString().ToLower(), new Location(pLocation, pLocation.ToString().ToLower(), pDescription, pBackground));
    }

    //add an exit to the game model
    private void AddWorldLocationExits(Location.NAME pFromLocation, Location.DIRECTION pDirection, Location.NAME pDestinationLocation)
    {
        worldMap[pFromLocation.ToString().ToLower()].exits.Add(pDirection, pDestinationLocation);                                    //assign exit to from location
        worldMap[pDestinationLocation.ToString().ToLower()].exits.Add(Location.GetOppositeDirection(pDirection), pFromLocation);     //assign reverse exit to destination location
    }

    //add an item to the game model
    private void AddWorldItem(Item.NAME pName, Location.NAME pLocation, string pSecretLetter)
    {
        worldItems.Add(pName.ToString().ToLower(), new Item(pName, pName.ToString().ToLower(), pLocation.ToString().ToLower(), pSecretLetter));
    }

    ////add an item to the game model
    //private void AddWorldItem(Item.NAME pName, Location.NAME pLocation, string pSecretLetter)
    //{
    //    //worldItems.Add(pName.ToString().ToLower(), new Item(pName, pName.ToString().ToLower(), pLocation.ToString().ToLower(), pSecretLetter));
    //    worldItems.Add(pName.ToString().ToLower(), new Item(pName, pName.ToString().ToLower(),
    //        pLocation.ToString().ToLower(), pSecretLetter, GameManager.instance.sessionId));
    //}

    public void LoadWorldMap()
    {

    }

    public void LoadWorldItems()
    {
        
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

//public void MakeWorldMap()
//{
//    locationMap = new Dictionary<string, Location>();

//    locationMap.Add(locationNames[0], new Location(locationNames[0], "A smelly tomb with a smelly corpse.", "sign3"));
//    locationMap[locationNames[0]].exits.Add(Location.DIRECTION.NORTH, locationNames[1]);

//    locationMap.Add(locationNames[1], new Location(locationNames[1], "A pretty beach with beautiful nothing to see.", "beach1"));
//    locationMap[locationNames[1]].exits.Add(Location.DIRECTION.SOUTH, locationNames[0]);

//    currentLocation = locationMap[locationNames[0]];

//    firstLocation = currentLocation;
//}

//public void MakeWorldMap()
//{
//    locationMap = new Dictionary<string, Location>();

//    locationMap.Add(locationNames[0], new Location(locationNames[0], "sign3"));
//    locationMap[locationNames[0]].description = "A smelly tomb with a smelly corpse.";

//    //locationMap["tomb"].items.Add(new Item("toenail", "a"));
//    //locationMap["tomb"].items.Add(new Item("armsling"));
//    locationMap[locationNames[0]].exits.Add(Location.DIRECTION.NORTH, locationNames[1]);

//    //now include some method which fetches this location's data and places it in a location DTO

//    locationMap.Add(locationNames[1], new Location(locationNames[1], "beach1"));
//    locationMap[locationNames[1]].description = "A pretty beach with beautiful nothing to see.";
//    //locationMap["beach"].items.Add(new Item("seashell"));
//    locationMap[locationNames[1]].exits.Add(Location.DIRECTION.SOUTH, locationNames[0]);

//    currentLocation = locationMap[locationNames[0]];

//    firstLocation = currentLocation;
//}


//private void MakeGameModel()
//{

//    //firstLocation = new Location("Smelly tomb", "sign3");
//    //firstLocation.locationItems.Add(new Item("Toenail", "a"));


//    //firstLocation.item.nextItem = new Item("Pigeon");

//    //firstLocation.North = new Location("Pretty beach", "beach1");
//    //firstLocation.North.item = new Item("Jar");
//    //firstLocation.North.South = firstLocation;

//    //firstLocation.West = new Location("Desperate Stall", "booth1");
//    //firstLocation.West.item = new Item("A Kiss");
//    //firstLocation.West.East = firstLocation;

//    //firstLocation.South = new Location("Unpleasant Village", "village1");
//    //firstLocation.South.North = firstLocation;

//    //firstLocation.East = new Location("Slippery Lake", "icelake");
//    //firstLocation.East.item = new Item("Snowflake", "q");
//    //firstLocation.East.West = firstLocation;

//    //firstLocation.East.East = new Location("Grassy Knoll", "grass1");
//    //firstLocation.East.East.West = firstLocation.East;
//    //firstLocation.East.East.West.West = firstLocation;

//    currentLocation = firstLocation;
//}
