using System;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;

public class CommandProcessor
{
    public Dictionary<string, Command> CommandMap;
    public string Result = "";

    public CommandProcessor()
    {
    }

    public string DetermineSceneOutput()
    {
        string lcOutputText = "Dont understand!";
        switch (GameManager.instance.CurrentUScene())       //determine output based on unity scene
        {
            case "GameScene":
                //lcOutputText = GameManager.instance.gameModel.currentScene.ToString();
                lcOutputText = GameManager.instance.gameModel.currentScene.DisplaySceneDetails();
                break;
            case "ItemScene":
                lcOutputText = GameManager.instance.gameModel.currentScene.SearchForItems();
                break;
            case "MapScene":
                lcOutputText = "Map of Beltora. Determine Scene Output";
                //Debug.Log("Map of Beltora. Determine Scene Output");
                break;
                //default:
                //    output.text = GameManager.instance.gameModel.currentScene.ToString();
                //    break;
        }
        return lcOutputText;
    }

    //parse user input into tokenised strings
    public String[] ParseInput(String pCommandString)
    {
        pCommandString = pCommandString.ToLower();
        return pCommandString.Split(' '); // tokenise the command
    }

    public String ProcessInput(String[] pCommandStrings)
    {
        CommandMap = new Dictionary<string, Command>();

        String lcResult = "Do not understand command";
        if (pCommandStrings.Length >= 2)                                //if no# command strings > 1
        {
            //UpdateCommandMap(pCommandStrings);
            UpdateCommandMap();
            if (CommandMap.ContainsKey(pCommandStrings[0]))             //if command is a mapped command
            {
                lcResult = RunCommand(pCommandStrings);                     //output string 'Result' of the mapped command
            }
            else                                                        //else if no mapped command is recognised
            {
                lcResult = DetermineSceneOutput() + "\n" + lcResult;    
            }
        }
        else                                                            //else if no# command strings < 2
        {
            lcResult = "Not enough words";
            lcResult = DetermineSceneOutput() + "\n" + lcResult;        //else output string of current story? How about if we are in map or inventory?
        }
        return lcResult;
    }

    private string RunCommand(string[] pCommandStrings)
    {
        string lcResult = "Do not understand!";
        string uSceneName = GameManager.instance.CurrentUScene();               //gets current Unity scene

        Command aCmd;
        aCmd = CommandMap[pCommandStrings[0]];
        aCmd.Do(pCommandStrings);                   //do the mapped command
        lcResult = aCmd.Result;

        lcResult = DetermineSceneOutput() + "\n" + lcResult;

        return lcResult;
    }

    private void UpdateCommandMap()
    {
        CommandMap.Add("answer", new AnswerCommand());
        CommandMap.Add("go", new GoCommand());
        CommandMap.Add("pick", new PickCommand());
        CommandMap.Add("show", new ShowCommand());
        CommandMap.Add("read", new ReadCommand());
    }

    //private void UpdateCommandMap(string[] pCommandStrings)
    //{
    //    CommandMap.Add("answer", new AnswerCommand(pCommandStrings));
    //    CommandMap.Add("go", new GoCommand(pCommandStrings));
    //    CommandMap.Add("pick", new PickCommand(pCommandStrings));
    //    CommandMap.Add("show", new ShowCommand(pCommandStrings));
    //    CommandMap.Add("read", new ReadCommand(pCommandStrings));
    //}
}


/* private List<String> Tokenise(String pCmdStr)
    {
        Regex regex = new Regex (@"\w\b");
        List<String> matchList = (from Match m in regex.Matches (pCmdStr)select m.Value).ToList ();

        return matchList;
    }
    */
