using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public Location currentLocation;
    public Dictionary<string, Location> locationMap;
    public List<Item> worldItems;
    public List<string> locationNames;

    public Location firstLocation;

    public GameModel()
    {
        locationNames = new List<string>
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

        MakeWorldMap();
        GenerateWorldItems();
    }

    public void MakeWorldMap()
    {
        locationMap = new Dictionary<string, Location>();

        locationMap.Add("tomb", new Location("tomb", "A smelly tomb with a smelly corpse.", "sign3"));
        locationMap.Add("beach", new Location("beach", "A pretty beach with beautiful nothing to see.", "beach1"));
        locationMap.Add("village", new Location("village", "A stupid village with stupid people.", "village1"));
        locationMap.Add("stall", new Location("stall", "An uncomfortable kissing booth.", "booth1"));
        locationMap.Add("house", new Location("house", "A stupid interior of a stupid house.", "house1"));
        locationMap.Add("forest", new Location("forest", "A dark, dark, musty forest.", "forest1"));
        //locationMap.Add("desert", new Location("desert", "A pretty beach with beautiful nothing to see.", "beach1"));


        locationMap["tomb"].exits.Add(Location.DIRECTION.NORTH, "beach");
        locationMap["tomb"].exits.Add(Location.DIRECTION.EAST, "forest");
        locationMap["tomb"].exits.Add(Location.DIRECTION.WEST, "village");


        locationMap["beach"].exits.Add(Location.DIRECTION.SOUTH, "tomb");
        locationMap["beach"].exits.Add(Location.DIRECTION.NORTH, "village");


        locationMap["forest"].exits.Add(Location.DIRECTION.WEST, "tomb");
        locationMap["forest"].exits.Add(Location.DIRECTION.EAST, "stall");

        locationMap["village"].exits.Add(Location.DIRECTION.EAST, "tomb");
        locationMap["village"].exits.Add(Location.DIRECTION.SOUTH, "beach");



        currentLocation = locationMap["tomb"];

        firstLocation = currentLocation;
    }

    private void GenerateWorldItems()
    {
        worldItems = new List<Item>();

        worldItems.Add(new Item("Jar", locationNames[0], "i"));
        worldItems.Add(new Item("Mop", locationNames[0], "l"));
        worldItems.Add(new Item("Shell", locationNames[1], "a"));
        worldItems.Add(new Item("Toe", locationNames[1], "z"));
        worldItems.Add(new Item("Helmet", locationNames[2], "b"));
        worldItems.Add(new Item("Cup", locationNames[3], "e"));
        worldItems.Add(new Item("Computer?", locationNames[4], "s"));
        worldItems.Add(new Item("Fireplace", locationNames[5], "t"));

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


    private void MakeGameModel()
    {

        //firstLocation = new Location("Smelly tomb", "sign3");
        //firstLocation.locationItems.Add(new Item("Toenail", "a"));


        //firstLocation.item.nextItem = new Item("Pigeon");

        //firstLocation.North = new Location("Pretty beach", "beach1");
        //firstLocation.North.item = new Item("Jar");
        //firstLocation.North.South = firstLocation;

        //firstLocation.West = new Location("Desperate Stall", "booth1");
        //firstLocation.West.item = new Item("A Kiss");
        //firstLocation.West.East = firstLocation;

        //firstLocation.South = new Location("Unpleasant Village", "village1");
        //firstLocation.South.North = firstLocation;

        //firstLocation.East = new Location("Slippery Lake", "icelake");
        //firstLocation.East.item = new Item("Snowflake", "q");
        //firstLocation.East.West = firstLocation;

        //firstLocation.East.East = new Location("Grassy Knoll", "grass1");
        //firstLocation.East.East.West = firstLocation.East;
        //firstLocation.East.East.West.West = firstLocation;

        currentLocation = firstLocation;
    }

}
