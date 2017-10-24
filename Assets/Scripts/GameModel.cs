using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    //public Dictionary<string, Location> locationMap;
    public Dictionary<Location.NAME, Location> worldMap;
    public Dictionary<Item.NAME, Item> worldItems;
    //public List<Item> worldItems;
    public Location firstLocation;
    public Location currentLocation;

    public static List<string> locationNames = new List<string>
    {
        "tomb",         //0
        "beach",        //1
        "village",      //2
        "stall",        //3
        "cliff",        //4
        "house",        //5
        "castle",       //6
        "forest",       //7
        "desert",       //8
        "plain",        //9
        "swamp",        //10
        "estuary",      //11
        "paddock",      //12
        "marsh"         //13
    };

    private DataService dataService;

    public GameModel()
    {
        GenerateWorldMap();
    }

    public void GenerateWorldMap()
    {
        worldMap = new Dictionary<Location.NAME, Location>();

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

        //worldMap[Location.NAME.TOMB].exits.Add(Location.DIRECTION.NORTH, Location.NAME.BEACH);
        //worldMap[Location.NAME.TOMB].exits.Add(Location.DIRECTION.EAST, Location.NAME.FOREST);
        //worldMap[Location.NAME.TOMB].exits.Add(Location.DIRECTION.WEST, Location.NAME.VILLAGE);

        //worldMap[Location.NAME.BEACH].exits.Add(Location.DIRECTION.SOUTH, Location.NAME.TOMB);
        //worldMap[Location.NAME.BEACH].exits.Add(Location.DIRECTION.NORTH, Location.NAME.VILLAGE);

        //worldMap[Location.NAME.FOREST].exits.Add(Location.DIRECTION.WEST, Location.NAME.TOMB);
        //worldMap[Location.NAME.FOREST].exits.Add(Location.DIRECTION.EAST, Location.NAME.STALL);

        //worldMap[Location.NAME.VILLAGE].exits.Add(Location.DIRECTION.EAST, Location.NAME.TOMB);
        //worldMap[Location.NAME.VILLAGE].exits.Add(Location.DIRECTION.SOUTH, Location.NAME.BEACH);


        //AddWorldLocation(Location.NAME.DESERT, "An uncomfortable kissing booth.", "booth1");

        //worldMap.Add(Location.NAME.TOMB, new Location(Location.NAME.TOMB, "tomb", "A smelly tomb with a smelly corpse.", "sign3"));
        //worldMap.Add(Location.NAME.BEACH, new Location(Location.NAME.BEACH, "beach", "A pretty beach with beautiful nothing to see.", "beach1"));
        //worldMap.Add(Location.NAME.VILLAGE, new Location(Location.NAME.VILLAGE, "village", "A stupid village with stupid people.", "village1"));
        //worldMap.Add(Location.NAME.STALL, new Location(Location.NAME.STALL, "stall", "An uncomfortable kissing booth.", "booth1"));
        //worldMap.Add(Location.NAME.HOUSE, new Location(Location.NAME.HOUSE, "house", "A stupid interior of a stupid house.", "house1"));
        //worldMap.Add(Location.NAME.FOREST, new Location(Location.NAME.FOREST, "forest", "A dark, dark, musty forest.", "forest1"));
        //locationMap.Add("desert", new Location("desert", "A pretty beach with beautiful nothing to see.", "beach1"));



        currentLocation = worldMap[Location.NAME.TOMB];

        firstLocation = currentLocation;        
    }


    public void GenerateWorldItems()
    {
        //worldItems = new List<Item>();
        worldItems = new Dictionary<Item.NAME, Item>();

        AddWorldItem(Item.NAME.JAR, Location.NAME.TOMB, "i");
        AddWorldItem(Item.NAME.MOP, Location.NAME.TOMB, "j");
        AddWorldItem(Item.NAME.COMPUTER, Location.NAME.HOUSE, "c");
        AddWorldItem(Item.NAME.CUP, Location.NAME.STALL, "a");
        AddWorldItem(Item.NAME.FIREPLACE, Location.NAME.HOUSE, "g");
        AddWorldItem(Item.NAME.HELMET, Location.NAME.PLAIN, "o");
        AddWorldItem(Item.NAME.SHELL, Location.NAME.BEACH, "p");
        AddWorldItem(Item.NAME.TOE, Location.NAME.VILLAGE, "r");


        //worldItems.Add(Item.NAME.JAR, new Item(Item.NAME.JAR, "Jar", locationNames[0], "i"));
        //worldItems.Add(new Item(itemCounter++, "Mop", locationNames[0], "l"));
        //worldItems.Add(new Item(itemCounter++, "Shell", locationNames[1], "a"));
        //worldItems.Add(new Item(itemCounter++, "Toe", locationNames[1], "z"));
        //worldItems.Add(new Item(itemCounter++, "Helmet", locationNames[2], "b"));
        //worldItems.Add(new Item(itemCounter++, "Cup", locationNames[3], "e"));
        //worldItems.Add(new Item(itemCounter++, "Computer?", locationNames[4], "s"));
        //worldItems.Add(new Item(itemCounter++, "Fireplace", locationNames[5], "t"));

    }

    private void AddWorldLocation(Location.NAME pLocation, string pDescription, string pBackground)
    {
        worldMap.Add(pLocation, new Location(pLocation, pLocation.ToString().ToLower(), pDescription, pBackground));
    }

    private void AddWorldLocationExits(Location.NAME pFromLocation, Location.DIRECTION pDirection, Location.NAME pDestinationLocation)
    {
        //assign exit to from location
        worldMap[pFromLocation].exits.Add(pDirection, pDestinationLocation);

        //assign reverse exit to destination location
        worldMap[pDestinationLocation].exits.Add(Location.GetOppositeDirection(pDirection), pFromLocation);
    }

    private void AddWorldItem(Item.NAME pName, Location.NAME pLocation, string pSecretLetter)
    {
        worldItems.Add(pName, new Item(pName, pName.ToString().ToLower(), pLocation.ToString().ToLower(), pSecretLetter));
    }

    public void LoadWorldMap()
    {

    }

    public void LoadWorldItems()
    {
        //dataService.
    }
}



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
