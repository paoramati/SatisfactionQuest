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
        Location.NAME nextLocation;
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
        int lcSessionId = GameManager.instance.sessionId;
        string lcSceneName = GameManager.GetCurrentScene();
        Location lcLocation = GameManager.instance.gameModel.currentLocation;
        var lcWorldItems = GameManager.instance.gameModel.worldItems;
        Item lcItem;

        DataService dataService = new DataService();

        if (lcSceneName == "ItemScene")
        {
            //return list of SessionItemDTOs
            Debug.Log("In Pick Up - Session ID = " + lcSessionId);

            Debug.Log("Username = " + GameManager.instance.player1.username);


            if (lcWorldItems.TryGetValue(pInputStrings[1], out lcItem))     //if a valid item is given
            {
                if (pInputStrings[1] == lcItem.name && lcItem.location == lcLocation.name)
                {
                    lcItem.location = GameManager.instance.player1.username;
                    dataService.SaveSessionItems(lcSessionId);
                    lcResult = "Picked up " + lcItem.name;
                    //dataService.UpdateItemLocation(lcItem.id, GameManager.instance.player1.username);   //can not discrimiate which player
                }

            }
            else
            {
                lcResult = "No item by that name here\n";
            }

            //    foreach (var item in lcWorldItems)
            //{
            //    Debug.Log("In Pick Up Before: Item ID = " + item.Value.id + " - Item Name = " + item.Value.name + " - ItemLocation = " + item.Value.location + " - SessionId = " + item.Value.sessionId);

            //    if (pInputStrings[1] == item.Value.name && item.Value.location == lcLocation.name)
            //    {
            //        dataService.UpdateItemLocation(item.Value.id, GameManager.instance.player1.username);   //can not discrimiate which player
            //    }
            //    else
            //    {
            //        lcResult = "No item by that name here\n";
            //    }
            //    Debug.Log("In Pick Up After: Item ID = " + item.Value.id + " - Item Name = " + item.Value.name + " - ItemLocation = " + item.Value.location + " - SessionId = " + item.Value.sessionId);

            //}


            //foreach (var item in dataService.GetSessionLocationItems(lcSessionId, lcLocation.name))
            //{
            //    if (item.Name == pInputStrings[1])
            //    {
            //        Debug.Log("In Pick Up Before - Item ID = " + item.ItemId + " - Item Name = " + item.Name + " - ItemLocation = " + item.Location);


            //        //dataService.UpdateSessionItem(item.Id);

            //        //update this sessions game items
            //        //THEN
            //        //load this change into this gameState's gameModel


            //        lcWorldItems[lcItemName].location = GameManager.instance.player1.username;

            //        Debug.Log("username = " + GameManager.instance.player1.username);

            //        dataService.SaveSessionItems(lcSessionId);

            //        //dataService.CreateSessionItems(lcSessionId);

            //        //Debug.Log("lcItemName = " + lcItemName);


            //        //Item.NAME lcItemName = (Item.NAME)

            //        //string lcSessionItemName = sessionItem.ItemName;


            //        //GameManager.instance.gameModel.worldItems[]

            //        //GameManager.instance.gameModel.worldItems[lcItemId].ChangeItemLocation(GameManager.instance.player1.username);

            //        //dataService.SaveSessionItems(lcSessionId);
            //        //GameManager.instance.SaveGameState();
            //        lcResult = "Picked up " + item.Name + "\n";

            //        Debug.Log("In Pick Up After - Item ID = " + item.ItemId + " - Item Name = " + item.Name + " - ItemLocation = " + item.Location);

            //        //GameManager.instance.gameModel.worldItems[]
            //        //update sessionItem location based on this item's Id
            //        //item.Id
            //        //GameManager.instance.gameModel.worldItems.FindIndex()
            //    }

        }
        else
        {
            lcResult = "Not in ItemScene\n";
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
            //Persist.control.Save();
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



