using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public Location currentLocation;
    public Dictionary<string, Location> locationMap;

    public Location firstLocation;

    //public enum LOCATION_NAMES
    //{   
    //    tomb, beach, VILLAGE, STALL, CLIFF, HOUSE, CASTLE, FOREST
    //};

    public List<string> locationNames;

    public GameModel()
    {
        locationNames = new List<string>
        {
            "tomb", "beach", "village", "stall", "cliff",
            "house", "castle", "forest", "desert", "plain",
            "swamp", "estuary", "paddock", "marsh"
        };

        MakeGameModel();

        MakeWorldMap();
    }

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

    public void MakeWorldMap()
    {

        

        locationMap = new Dictionary<string, Location>();

        locationMap.Add("tomb", 
            new Location("tomb", "sign3"));
        locationMap["tomb"].items.Add(new Item("toenail", "a"));
        locationMap["tomb"].items.Add(new Item("armsling"));
        locationMap["tomb"].description = "A smelly tomb with a smelly corpse.";

        locationMap["tomb"].exits.Add(Location.DIRECTION.NORTH, "beach");

        locationMap.Add("beach", new Location("beach", "beach1"));
        locationMap["beach"].items.Add(new Item("seashell"));
        locationMap["tomb"].exits.Add(Location.DIRECTION.SOUTH, "tomb");

        currentLocation = locationMap["tomb"];

        firstLocation = currentLocation;
    }
}
