using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{// : MonoBehaviour {

    private Command next;
    public string Result;

    // Use this for initialization
    void Awake() { }

    public virtual void Initialise(string[] pAdverbs) { }

    //public virtual void Do(CommandMap pCommand) { }

    public virtual void Do(string[] pAdverbs) { }
}

public class GoCommand : Command
{
    private Dictionary<string, string> goCommands;

    private string adverb;
    private string[] adverbs;

    public GoCommand()
    {

    }

    public GoCommand(string pAdverb)
    {
        adverb = pAdverb;
    }

    public GoCommand(string[] pAdverbs)
    {
        goCommands = new Dictionary<string, string>
        {
            { "north", "north" },
            { "south", "south" },
            { "east", "east" },
            { "west", "west" }
        };
    }

    public override void Do(string[] pAdverbs)
    {
        Debug.Log("Got a Go " + pAdverbs[1]);

        Scene lcScene = GameManager.instance.gameModel.currentScene;
        GameManager.instance.gameModel.currentScene.sceneStatus = "";           //reset location scene status
        bool sceneExists = false;                                               //checks whether a location scene exists at the given direction
        string uSceneName = GameManager.instance.currentUScene();               //gets current Unity scene
        if (uSceneName == "GameScene" && goCommands.ContainsKey(pAdverbs[1]))   //filters out not-allowed adverbs
        //if (uSceneName == "GameScene")
        {
            switch (pAdverbs[1])
            {
                case "north":
                    lcScene = GameManager.instance.gameModel.currentScene;
                    if (lcScene.North != null)
                    {
                        GameManager.instance.gameModel.currentScene = lcScene.North;
                        sceneExists = true;
                    }

                    break;
                case "south":
                    lcScene = GameManager.instance.gameModel.currentScene;
                    if (lcScene.South != null)
                    {
                        GameManager.instance.gameModel.currentScene = lcScene.South;
                        sceneExists = true;
                    }
                    break;
                case "east":
                    lcScene = GameManager.instance.gameModel.currentScene;
                    if (lcScene.East != null)
                    {
                        GameManager.instance.gameModel.currentScene = lcScene.East;
                        sceneExists = true;
                    }
                    break;
                case "west":
                    lcScene = GameManager.instance.gameModel.currentScene;
                    if (lcScene.West != null)
                    {
                        GameManager.instance.gameModel.currentScene = lcScene.West;
                        sceneExists = true;
                    }
                    break;
                //default:
                //    lcscene = GameManager.instance.gameModel.currentscene;

            }
            if (sceneExists == false)
                GameManager.instance.gameModel.currentScene.sceneStatus = "Nowhere to go in that direction";
            //    Result += "\nNo scene exists in that direction.";
            Debug.Log("Scene number = " + lcScene.story);
        }
        else
            Result = "Not able to go places when in " + uSceneName;
    }

}

/*
 * ShowCommand changes Unity scenes depending on context
 */
public class ShowCommand : Command
{
    private string adverb;

    public ShowCommand() { }

    public ShowCommand(string pAdverb)
    {
        adverb = pAdverb;
    }

    public ShowCommand(string[] pAdverbs)
    {
    }

    public override void Do(string[] pAdverbs)
    {
        Debug.Log("Got a Show" + adverb);
        string lcResult = "Do not understand. Did you mean \"show items\", or \"show scene\"?";
        Scene lcScene = GameManager.instance.gameModel.currentScene;
        string lcUnityScene = GameManager.instance.currentUScene();



        switch (pAdverbs[1])
        {
            case "items":
                GameManager.instance.gameModel.currentScene = lcScene;

                GameManager.instance.changeUScene("ItemScene");
                Result = lcScene.searchForItems();
                break;
            case "scene":
                GameManager.instance.gameModel.currentScene = lcScene;
                GameManager.instance.changeUScene("GameScene");
                Result = GameManager.instance.gameModel.currentScene.ToString();
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



