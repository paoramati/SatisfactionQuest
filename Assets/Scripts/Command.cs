using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{// : MonoBehaviour {

    private Command next;
    // Use this for initialization
    void Awake()
    {

    }

    public virtual void Do(CommandMap aCmd)
    {
    }


}

public class GoCommand : Command
{
    private string adverb;

    public GoCommand(string pAdverb)
    {
        adverb = pAdverb;
    }

    public override void Do(CommandMap pCmd)
    {
        Debug.Log("Got a Go" + adverb);

        Location lcLocation = GameManager.instance.gameModel.currentLocation;
        string uSceneName = GameManager.instance.currentUScene();
        if (uSceneName == "TextIO")
        {
            switch (adverb)
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

            pCmd.Result = GameManager.instance.gameModel.currentLocation.Story;
        }
        else
            pCmd.Result = "Not able to go places when in " + uSceneName;

    }
}

public class PickCommand : Command
{
    private string adverb;

    public PickCommand(string pAdverb)
    {
        adverb = pAdverb;
    }

    public override void Do(CommandMap pCmd)
    {
        Debug.Log("Got a Pick" + adverb);

    }
}


public class ShowCommand : Command
{
    private string adverb;

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

