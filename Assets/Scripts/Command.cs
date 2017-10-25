using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    private Command next;
    public string Result;
    public string[] inputCommands;


    public virtual void Do(string[] pInputStrings) { }
}

public class GoCommand : Command
{
    private Dictionary<string, Location.DIRECTION> goCommands;      //valid direction adverbs mapped to proper direction enums

    public GoCommand()
    {
        goCommands = new Dictionary<string, Location.DIRECTION>
        {
            { "north", Location.DIRECTION.NORTH },
            { "south", Location.DIRECTION.SOUTH },
            { "east", Location.DIRECTION.EAST },
            { "west", Location.DIRECTION.WEST },
            { "up", Location.DIRECTION.NORTH },
            { "down", Location.DIRECTION.SOUTH },
            { "left", Location.DIRECTION.WEST },
            { "right", Location.DIRECTION.EAST }
        };
    }

    public override void Do(string[] pInputStrings)
    {
        string lcResult = "";
        Debug.Log("Got a Go " + pInputStrings[1]);
        Location lcLocation = GameManager.instance.gameModel.currentLocation;
        string lcSceneName = GameManager.GetCurrentScene();
        Location.DIRECTION lcDirection;
        string nextLocation;
        //string nextLocationName;

        if (lcSceneName == "GameScene")
        {
            if (goCommands.TryGetValue(pInputStrings[1], out lcDirection))       //if a correctly defined direction is given
            {
                if (lcLocation.exits.TryGetValue(lcDirection, out nextLocation))       //if current location has exit at this direction
                {
                    GameManager.instance.gameModel.currentLocation = GameManager.instance.gameModel.worldMap[nextLocation.ToString().ToLower()];
                }
                else
                {
                    lcResult = ">Nowhere to go in that direction";
                }
            }
            else    //direction input is not correct                                                  
            {
                lcResult = ">That is not a direction";
            }
        }
        else    //Unity scene is not GameScene
        {
            lcResult = ">Not able to go places when in " + lcSceneName;
        }
        Result = lcResult;
    }
}

public class ShowCommand : Command
{
    public ShowCommand() { }

    public override void Do(string[] pInputStrings)
    {
        Debug.Log("Got a Show " + pInputStrings[1]);
        Location lcLocation = GameManager.instance.gameModel.currentLocation;
        string lcSceneName = GameManager.GetCurrentScene();
        string lcResult = "";

        switch (pInputStrings[1])
        {
            case "exits":
                lcResult = lcLocation.GetLocationExits();
                break;
            case "items":
                GameManager.ChangeScene("ItemScene");
                break;
            case "location":
                GameManager.ChangeScene("GameScene");
                break;
            case "inventory":
                GameManager.ChangeScene("InventoryScene");
                break;
            case "map":
                GameManager.ChangeScene("MapScene");
                break;
            case "help":
                GameManager.ChangeScene("HelpScene");
                break;
            default:
                lcResult = ">Do not understand. Valid 'show' commands: 'exits', 'location', 'items', 'map', 'help'";
                break;
        }
        Result = lcResult;
    }
}

public class PickCommand : Command
{
    private string adverb;

    public PickCommand() { }

    public override void Do(string[] pInputStrings)
    {
        Debug.Log("Got a Pick " + pInputStrings[1]);

        string lcResult = "";
        Player lcPlayer = GameManager.instance.player1;
        int lcSessionId = GameManager.instance.sessionId;
        Location lcLocation = GameManager.instance.gameModel.currentLocation;
        var lcWorldItems = GameManager.instance.gameModel.worldItems;
        Item lcItem;

        DataService dataService = new DataService();

        if (GameManager.GetCurrentScene() == "ItemScene")
        {
            if (lcWorldItems.TryGetValue(pInputStrings[1], out lcItem))                         //if a valid item is given
            {
                if (pInputStrings[1] == lcItem.name && lcItem.location == lcLocation.name)
                {
                    Debug.Log("lcItem: " + lcItem.id + " - " + lcItem.name + " - " + lcItem.sessionId + " - " + lcItem.location);

                    lcItem.location = lcPlayer.username;        //change location from location name to player name

                    Debug.Log("lcItem: " + lcItem.id + " - " + lcItem.name + " - " + lcItem.sessionId + " - " + lcItem.location);

                    //saving the session items here is creating duplicate item sets. how to avoid this?

                    dataService.SaveSessionItems();
                    //dataService.LoadSessionItems();
                    lcPlayer.esteem += 10;
                    lcResult = "Picked up " + lcItem.name;
                }
            }
            else
            {
                lcResult = "No item by that name here";
            }
        }
        else
        {
            lcResult = "Not in ItemScene";
        }
        Result = lcResult;
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

    public override void Do(string[] pInputStrings)
    {
        Debug.Log("Answer " + pInputStrings[1]);
    }
}

public class ReadCommand : Command
{
    public ReadCommand() { }

    public ReadCommand(string[] pAdverbs)
    {
    }

    public override void Do(string[] pInputStrings)
    {
        Debug.Log("Got a Show " + pInputStrings[1]);
        Location lcLocation = GameManager.instance.gameModel.currentLocation;
        string lcSceneName = GameManager.GetCurrentScene();
        string lcResult = "";

        if (pInputStrings[1] == "health")
        {
            //Persist.control.Health += 10;
            //lcResult = "Health = " + Persist.control.Health;
        }

        Result = lcResult;
    }
}

public class SaveCommand : Command
{

    public SaveCommand() { }



    public SaveCommand(string[] pAdverbs)
    {
    }

    public override void Do(string[] pInputStrings)
    {
        Debug.Log("Save " + pInputStrings[1]);

        if (pInputStrings[1] == "game")
        {
            DataService dataService = new DataService();

            //save current location
            //save player state
            //save session items
            //inform player game is saved

            Application.Quit();
        }


    }
}

public class QuitCommand : Command
{

    public QuitCommand() { }



    public QuitCommand(string[] pAdverbs)
    {
    }

    public override void Do(string[] pInputStrings)
    {
        Debug.Log("Quit " + pInputStrings[1]);


    }
}



