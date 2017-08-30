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

    public virtual void Do(CommandMap pCommand) { }

    public virtual void Do(string[] pAdverbs) { }
}

public class GoCommand : Command
{
    private Dictionary<string, string> goCommands;

    private string adverb;
    private string[] adverbs;

    private enum Directions
    {
        north, south, east, west
    }

    public GoCommand()
    {   
        goCommands = new Dictionary<string, string>();
        goCommands.Add("north", "north");
        goCommands.Add("south", "south");
        goCommands.Add("east", "east");
        goCommands.Add("west", "west");
    }

    public GoCommand(string pAdverb)
    {
        adverb = pAdverb;
    }

    public GoCommand(string[] pAdverbs)
    {
    }

    public override void Do(string[] pAdverbs)
    {
        Debug.Log("Got a Go " + pAdverbs[1]);

        Location lcLocation = GameManager.instance.gameModel.currentLocation;
        string uSceneName = GameManager.instance.currentUScene();               //gets current Unity scene
        //if (uSceneName == "GameScene" && goCommands.ContainsKey(pAdverbs[1]))   //hopefully also filters out not-allowed adverbs
        if (uSceneName == "GameScene")
        {

            switch (pAdverbs[1])
            {
                case "north":
                    lcLocation = GameManager.instance.gameModel.currentLocation;
                    if (lcLocation.North != null)
                        GameManager.instance.gameModel.currentLocation = lcLocation.North;
                    break;
                case "south":
                    lcLocation = GameManager.instance.gameModel.currentLocation;
                    if (lcLocation.South != null)
                        GameManager.instance.gameModel.currentLocation = lcLocation.South;
                    break;
                case "east":
                    lcLocation = GameManager.instance.gameModel.currentLocation;
                    if (lcLocation.East != null)
                        GameManager.instance.gameModel.currentLocation = lcLocation.East;
                    break;
                case "west":
                    lcLocation = GameManager.instance.gameModel.currentLocation;
                    if (lcLocation.West != null)
                        GameManager.instance.gameModel.currentLocation = lcLocation.West;
                    break;
            }
            
            Result = GameManager.instance.gameModel.currentLocation.Story;
        }
        else
            Result = "Not able to go places when in " + uSceneName;
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

    public override void Do(CommandMap pCommand)
    {
        Debug.Log("Got a Pick" + adverb);

        //check if item adverb supplied matches items available at current location


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

    public override void Do(CommandMap aCmd)
    {
        string lcResult = "Do not understand you answer!";
        Debug.Log("Got an Answer" + Answer);

        if (Answer == GameManager.instance.gameModel.currentLocation.Answer)
        {

        }

        base.Do(aCmd);
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

    public override void Do(CommandMap pCmd)
    {
        string lcResult = "Do not understand. Did you mean \"show items\", or \"show scene\"?";

        Debug.Log("Got a Show" + adverb);
        //switch (adverb)
        //{
        //    case "items":

        //        // Collect the items into one list
        //        lcResult = GameManager.instance.gameModel.lcLocation.allItems();
        //        //GameManager.instance.changeUScene ("ItemsScene");
        //        GameManager.instance.setActiveCanvas("ItemsCanvas");
        //        break;
        //    case "scene":

        //        lcResult = GameManager.instance.gameModel.currentScene.Story;
        //        //GameManager.instance.changeUScene ("TextIO");
        //        GameManager.instance.setActiveCanvas("GameCanvas");
        //        break;
        //}
        //pCmd.Result = lcResult;
    }
}

public class ReadCommand : Command
{
    public ReadCommand() { }
}



