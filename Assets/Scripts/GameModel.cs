using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public Location currentLocation;
    public Dictionary<string, Location> worldMap;
    public Location firstLocation;

    public GameModel()
    {
        MakeGameModel();
    }

    private void MakeGameModel()
    {
        //firstLocation = new Location("Smelly Tomb", "sign3");
        //firstLocation.item = new Item("Toenail", "a");
        //firstLocation.item.nextItem = new Item("Pigeon");

        //firstLocation.North = new Location("Pretty Beach", "beach1");
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
