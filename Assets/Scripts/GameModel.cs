using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel {

    public Location currentLocation;
    public Location firstLocation;

    private void makeStory()
    {
        firstLocation = new Location("Ho, Adventurer! You awake at a Smelly Tomb. To the north is a pretty beach.");
		//firstLocation.BackgroundImage.spri

        firstLocation.North = new Location("You at a pretty beach. To the south is a smelly tomb.");
        firstLocation.North.South = firstLocation;


        currentLocation = firstLocation;
    }

    public GameModel()
    {
        makeStory();
    }

}
