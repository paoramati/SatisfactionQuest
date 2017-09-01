using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel {

    public Scene currentScene;
    public Scene firstScene;
    public Sprite backgroundImage;
    public ChangeImage backgroundChanger;

    private void makeStory()
    {
        firstScene = new Scene("Ho, Adventurer! You awake at the Smelly Tomb. To the north is a pretty beach. To the East is a stupid stable");
        //firstScene = new Scene("Ho, Adventurer! You awake at a Smelly Tomb. To the north is a pretty beach.", "beach1");
        firstScene.backgroundImageName = "beach1";
        firstScene.item = new Item("Shell", "a");
        firstScene.item.nextItem = new Item("Seagull");

        firstScene.question = "What is the magic word?";
        //firstScene.BackgroundImage = Resources.Load<Sprite>("beach1");
        //firstscene.BackgroundImage.spri

        firstScene.North = new Scene("You at a pretty beach. To the south is the Smelly Tomb.");
        //firstScene.North = new Scene("You at a pretty beach. To the south is a smelly tomb.", "forest1");
        firstScene.North.backgroundImageName = "forest1";
        firstScene.North.South = firstScene;
        firstScene.North.item = new Item("Jar");
        firstScene.North.Answer = 10;
        //firstscene.North.Question = 

        firstScene.East = new Scene("You at a stupid stable. To the West is the Smelly Tomb. To the East is a grassy knoll.");
        firstScene.East.West = firstScene;

        firstScene.East.East = new Scene("You at a grassy knoll. To the West is a stupid stable.");
        firstScene.East.East.West = firstScene;


        currentScene = firstScene;
    }

    public GameModel()
    {
        makeStory();
    }

    public void changeScene(Scene pScene)
    {

    }

}
