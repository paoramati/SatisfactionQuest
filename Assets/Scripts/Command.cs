using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    private Command next;
    public string Result;

    // Use this for initialization
    void Awake() { }

    public virtual void Initialise(string[] pInputStrings) { }

    public virtual void Do(string[] pInputStrings) { }
}

public class GoCommand : Command
{
    bool sceneExists;                                               //checks whether a location scene exists at the given direction
    private Dictionary<string, string> goCommands;

//    public GoCommand() { }

    public GoCommand()
    {
        sceneExists = false;
        goCommands = new Dictionary<string, string>
        {
            { "north", "north" },
            { "south", "south" },
            { "east", "east" },
            { "west", "west" }
        };
    }

    public override void Do(string[] pInputStrings)
    {
        Debug.Log("Got a Go " + pInputStrings[1]);
        Scene lcScene = GameManager.instance.gameModel.currentScene;
        string lcUSceneName = GameManager.instance.CurrentUScene();              //gets current Unity scene

        //GameManager.instance.gameModel.currentScene.sceneStatus = "";           //reset location scene status

        if (lcUSceneName == "GameScene")                                        //if in gameScene
        {
            if (goCommands.ContainsKey(pInputStrings[1]))                            //if a correct direction is given
            {
                switch (pInputStrings[1])
                {
                    case "north":
                        if (lcScene.North != null)
                        {
                            GameManager.instance.gameModel.currentScene = lcScene.North;
                            sceneExists = true;
                        }
                        break;
                    case "south":
                        if (lcScene.South != null)
                        {
                            GameManager.instance.gameModel.currentScene = lcScene.South;
                            sceneExists = true;
                        }
                        break;
                    case "east":
                        if (lcScene.East != null)
                        {
                            GameManager.instance.gameModel.currentScene = lcScene.East;
                            sceneExists = true;
                        }
                        break;
                    case "west":
                        if (lcScene.West != null)
                        {
                            GameManager.instance.gameModel.currentScene = lcScene.West;
                            sceneExists = true;
                        }
                        break;
                }
                if (sceneExists == false)
                    Result = "Nowhere to go in that direction";
            }
            else  //if direction is not correct
            {
                Result = "That is not a direction";
            }
        }
        else
            Result = "Not able to go places when in " + lcUSceneName;
    }
}

/*
 * ShowCommand changes Unity scenes depending on context
 */
public class ShowCommand : Command
{
    //private string adverb;

    public ShowCommand() { }

    public ShowCommand(string[] pInputStrings)
    {
    }

    public override void Do(string[] pInputStrings)
    {
        Debug.Log("Got a Show" + pInputStrings[1]);
        string lcResult = "";
        Scene lcScene = GameManager.instance.gameModel.currentScene;
        string lcUSceneName = GameManager.instance.CurrentUScene();

        switch (pInputStrings[1])
        {
            case "items":
                GameManager.instance.ChangeUScene("ItemScene");
                break;
            case "scene":
                GameManager.instance.ChangeUScene("GameScene");
                break;
            case "map":
                GameManager.instance.ChangeUScene("MapScene");
                break;
            default:
                lcResult = "Do not understand. Valid 'show' commands: 'scene', 'items', 'map'";
                break;
        }
        Result = lcResult;
    }
}

public class PickCommand : Command
{
    private string adverb;

    public PickCommand() { }

    public PickCommand(string pAdverb)
    {
        adverb = pAdverb;
    }

    public PickCommand(string[] pAdverbs)
    {
    }

}

public class AnswerCommand : Command
{
    private int Answer;

    public AnswerCommand() { }

    public AnswerCommand(int pAnswer)
    {
        Answer = pAnswer;
    }

    public AnswerCommand(string[] pAdverbs)
    {
    }


}



public class ReadCommand : Command
{
    public ReadCommand() { }

    public ReadCommand(string[] pAdverbs)
    {
    }
}



