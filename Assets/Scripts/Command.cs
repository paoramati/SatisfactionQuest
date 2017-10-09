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
    private bool locationExists;                                       //checks whether a location exists at the given direction
    //private Dictionary<string, string> goCommands;
    private Dictionary<string, Location.DIRECTION> goCommands;

    public GoCommand()
    {
        //goCommands = new Dictionary<string, string>
        //{
        //    { "north", "north" },
        //    { "south", "south" },
        //    { "east", "east" },
        //    { "west", "west" },
        //    { "up", "north" },
        //    { "down", "south" },
        //    { "left", "west" },
        //    { "right", "east" }
        //};

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
        Debug.Log("Got a Go " + pInputStrings[1]);
        locationExists = false;
        Location lcLocation = GameManager.instance.gameModel.currentLocation;
        string lcSceneName = GameManager.GetCurrentScene();             
        Location.DIRECTION lcDirection;
        string nextLocationName;
        if (lcSceneName == "GameScene")                                       
        {



            if (goCommands.TryGetValue(pInputStrings[1], out lcDirection))       //if a correctly defined direction is given
            //if (goCommands.ContainsKey(pInputStrings[1]))
            {
                if (lcLocation.exits.TryGetValue(lcDirection, out nextLocationName ))       //if current location has exit at this direction
                {
                    //GameManager.instance.gameModel.locationMap[GameManager.instance.gameModel.currentLocation.exits[lcDirection]];
                    GameManager.instance.gameModel.currentLocation = GameManager.instance.gameModel.locationMap[nextLocationName];
                }
                else
                {
                    Result = ">Nowhere to go in that direction";
                }


                //if (lcLocation.exits.TryGetValue(lcLocation.exits[)


                //switch (lcDirection)
                //{
                //    case "north":
                //        if (lcLocation.South != null)
                //        {
                //            GameManager.instance.gameModel.currentLocation = lcLocation.North;
                //            locationExists = true;
                //        }
                //        break;
                //    case "south":
                //        if (lcLocation.South != null)
                //        {
                //            GameManager.instance.gameModel.currentLocation = lcLocation.South;
                //            locationExists = true;
                //        }
                //        break;
                //    case "east":
                //        if (lcLocation.East != null)
                //        {
                //            GameManager.instance.gameModel.currentLocation = lcLocation.East;
                //            locationExists = true;
                //        }
                //        break;
                //    case "west":
                //        if (lcLocation.West != null)
                //        {
                //            GameManager.instance.gameModel.currentLocation = lcLocation.West;
                //            locationExists = true;
                //        }
                //        break;
                //}
                //if (locationExists == false)                        
                    
            }
            else  //direction input is not correct                                                  
                Result = ">That is not a direction";
        }
        else 
            Result = ">Not able to go places when in " + lcSceneName;
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
            case "items":
                GameManager.ChangeScene("ItemScene");
                break;
            case "location":
                GameManager.ChangeScene("GameScene");
                break;
            case "map":
                GameManager.ChangeScene("MapScene");
                break;
            case "help":
                GameManager.ChangeScene("HelpScene");
                break;
            default:
                lcResult = ">Do not understand. Valid 'show' commands: 'location', 'items', 'map', 'help'";
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

    public override void Do(string[] pInputStrings)
    {
        Debug.Log("Pick " + pInputStrings[1]);
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
            Persist.control.Health += 10;
            lcResult = "Health = " + Persist.control.Health;
        }

        Result = lcResult;
    }
}

public class SaveCommand : Command
{
    private int Answer;

    public SaveCommand() { }



    public SaveCommand(string[] pAdverbs)
    {
    }

    public override void Do(string[] pInputStrings)
    {
        Debug.Log("Save " + pInputStrings[1]);

        if (pInputStrings[1] == "game")
        {
            Persist.control.Save();
            Application.Quit();
        }


    }
}

public class QuitCommand : Command
{
    private int Answer;

    public QuitCommand() { }



    public QuitCommand(string[] pAdverbs)
    {
    }

    public override void Do(string[] pInputStrings)
    {
        Debug.Log("Quit " + pInputStrings[1]);
            
            
    }
}



