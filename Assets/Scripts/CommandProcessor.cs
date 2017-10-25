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
        CommandMap = new Dictionary<string, Command>();
        CommandMap.Add("go", new GoCommand());
        CommandMap.Add("answer", new AnswerCommand());
        CommandMap.Add("pick", new PickCommand());
        CommandMap.Add("pickup", new PickCommand());
        CommandMap.Add("take", new PickCommand());
        CommandMap.Add("show", new ShowCommand());
        CommandMap.Add("read", new ReadCommand());
        CommandMap.Add("quit", new QuitCommand());
        CommandMap.Add("save", new SaveCommand());
    }

    public string GetSceneOutput()
    {
        string lcOutputText = "";
        Location lcLocation = GameManager.instance.gameModel.currentLocation;
        Player lcPlayer = GameManager.instance.Player1;

        switch (GameManager.GetCurrentScene())       
        {
            case "GameScene":
                lcOutputText = lcLocation.GetLocationDetails();
                break;
            case "ItemScene":
                //DataServiceUtilities.RefreshGameSession();
                lcOutputText = lcLocation.GetLocationItems();
                break;
            case "InventoryScene":
                //DataServiceUtilities.RefreshGameSession();
                lcOutputText = lcPlayer.GetInventoryItems();
                break;
            //case "MapScene":
            //    lcOutputText = "MAP OF BELTORA.\nCurrent location: " + lcLocation.name;
            //    break;
            case "HelpScene":
                lcOutputText = "HOW TO PLAY: Enter commands to perform various actions \n\nCOMMANDS:\n";
                lcOutputText += "'Go [direction]' - Move player location \n\t[direction] = north | south | east | west | up | down | left | right\n";
                lcOutputText += "'Show [scene]'   - Change game scene \n\t[scene] = location | items | inventory | help\n";
                lcOutputText += "'Pick | Take [object]'   - Retrieve \n\t[object] = any item visible in ItemScene\n";
                lcOutputText += "'Save game'   - Save current game state from any screen\n";
                break;
        }
        return lcOutputText;
    }

    public String[] ParseInput(String pCommandString)
    {
        pCommandString = pCommandString.ToLower();
        return pCommandString.Split(' '); 
    }

    public String ProcessInput(String[] pCommandStrings)
    {
        String lcResult = ">Do not understand command. Enter 'show help' to view valid commands";
        if (pCommandStrings.Length >= 1)                                
        {
            if (CommandMap.ContainsKey(pCommandStrings[0]))            
                lcResult = RunCommand(pCommandStrings);                
            else                                                       
                lcResult = GetSceneOutput() + "\n" + lcResult;    
        }
        else                                                           
            lcResult = GetSceneOutput() + "\n" + ">Not enough words";       
        return lcResult;
    }

    private string RunCommand(string[] pCommandStrings)
    {
        string lcResult = "";
        Command aCmd;
        aCmd = CommandMap[pCommandStrings[0]];
        aCmd.Do(pCommandStrings);                                          
        lcResult = aCmd.Result;
        lcResult = GetSceneOutput() + "\n" + lcResult;
        return lcResult;
    }
}