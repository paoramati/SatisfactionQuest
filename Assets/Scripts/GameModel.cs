using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public Scene currentScene;
    public Scene firstScene;

    public GameModel()
    {
        MakeStory();
    }

    private void MakeStory()
    {
        firstScene = new Scene("Smelly Tomb", "sign3");
        firstScene.item = new Item("Toenail", "a");
        firstScene.item.nextItem = new Item("Pigeon");

        firstScene.North = new Scene("Pretty Beach", "beach1");
        firstScene.North.item = new Item("Jar");
        firstScene.North.South = firstScene;

        firstScene.West = new Scene("Desperate Stall", "booth1");
        firstScene.West.item = new Item("A Kiss");
        firstScene.West.East = firstScene;

        firstScene.South = new Scene("Unpleasant Village", "village1");
        firstScene.South.North = firstScene;

        firstScene.East = new Scene("Slippery Lake", "icelake");
        firstScene.East.item = new Item("Snowflake", "q");
        firstScene.East.West = firstScene;

        firstScene.East.East = new Scene("Grassy Knoll", "grass1");
        firstScene.East.East.West = firstScene.East;
        firstScene.East.East.West.West = firstScene;

        currentScene = firstScene;
    }
}
