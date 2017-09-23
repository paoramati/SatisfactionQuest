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
    bool locationExists;                                       //checks whether a location exists at the given direction
    private Dictionary<string, string> goCommands;

    public GoCommand()
    {
        locationExists = false;
        goCommands = new Dictionary<string, string>
        {
            { "north", "north" },
            { "south", "south" },
            { "east", "east" },
            { "west", "west" },
            { "up", "north" },
            { "down", "south" },
            { "left", "west" },
            { "right", "east" }
        };
    }

    public override void Do(string[] pInputStrings)
    {
        Debug.Log("Got a Go " + pInputStrings[1]);
<<<<<<< HEAD
        Location lcLocation = GameManager.gameStateInstance.gameModel.currentLocation;
        string lcSceneName = GameManager.instance.GetCurrentScene();             
=======
        Location lcLocation = GameManager._Instance._GameModel.currentLocation;
        string lcSceneName = GameManager._Instance.GetCurrentScene();             
>>>>>>> parent of f2f3a4c... added LoginController
        string lcDirection = "";
        if (lcSceneName == "GameScene")                                       
        {
            if (goCommands.TryGetValue(pInputStrings[1], out lcDirection))       //if a correctly defined direction is given
            {
                switch (lcDirection)
                {
                    case "north":
                        if (lcLocation.North != null)
                        {
<<<<<<< HEAD
                            GameManager.gameStateInstance.gameModel.currentLocation = lcLocation.North;
=======
                            GameManager._Instance._GameModel.currentLocation = lcLocation.North;
>>>>>>> parent of f2f3a4c... added LoginController
                            locationExists = true;
                        }
                        break;
                    case "south":
                        if (lcLocation.South != null)
                        {
<<<<<<< HEAD
                            GameManager.gameStateInstance.gameModel.currentLocation = lcLocation.South;
=======
                            GameManager._Instance._GameModel.currentLocation = lcLocation.South;
>>>>>>> parent of f2f3a4c... added LoginController
                            locationExists = true;
                        }
                        break;
                    case "east":
                        if (lcLocation.East != null)
                        {
<<<<<<< HEAD
                            GameManager.gameStateInstance.gameModel.currentLocation = lcLocation.East;
=======
                            GameManager._Instance._GameModel.currentLocation = lcLocation.East;
>>>>>>> parent of f2f3a4c... added LoginController
                            locationExists = true;
                        }
                        break;
                    case "west":
                        if (lcLocation.West != null)
                        {
<<<<<<< HEAD
                            GameManager.gameStateInstance.gameModel.currentLocation = lcLocation.West;
=======
                            GameManager._Instance._GameModel.currentLocation = lcLocation.West;
>>>>>>> parent of f2f3a4c... added LoginController
                            locationExists = true;
                        }
                        break;
                }

                if (locationExists == false)                        
                    Result = ">Nowhere to go in that direction";
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
<<<<<<< HEAD
        Location lcLocation = GameManager.gameStateInstance.gameModel.currentLocation;
        string lcSceneName = GameManager.instance.GetCurrentScene();
=======
        Location lcLocation = GameManager._Instance._GameModel.currentLocation;
        string lcSceneName = GameManager._Instance.GetCurrentScene();
>>>>>>> parent of f2f3a4c... added LoginController
        string lcResult = "";

        switch (pInputStrings[1])
        {
            case "items":
                GameManager._Instance.ChangeScene("ItemScene");
                break;
            case "location":
                GameManager._Instance.ChangeScene("GameScene");
                break;
            case "map":
                GameManager._Instance.ChangeScene("MapScene");
                break;
            case "help":
                GameManager._Instance.ChangeScene("HelpScene");
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
<<<<<<< HEAD
        Location lcLocation = GameManager.gameStateInstance.gameModel.currentLocation;
        string lcSceneName = GameManager.instance.GetCurrentScene();
=======
        Location lcLocation = GameManager._Instance._GameModel.currentLocation;
        string lcSceneName = GameManager._Instance.GetCurrentScene();
>>>>>>> parent of f2f3a4c... added LoginController
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



