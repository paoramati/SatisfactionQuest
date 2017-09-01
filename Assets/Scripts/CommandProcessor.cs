using System;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;

public class CommandProcessor
{
	public CommandProcessor ()
	{
	}

    /* private List<String> Tokenise(String pCmdStr)
		{
			Regex regex = new Regex (@"\w\b");
			List<String> matchList = (from Match m in regex.Matches (pCmdStr)select m.Value).ToList ();

			return matchList;
		}
		*/

    //parse user input into tokenised strings
    public String[] ParseInput(String pCommandString) {
        pCommandString = pCommandString.ToLower();
        return pCommandString.Split(' '); // tokenise the command
    }


    public String ProcessInput(String[] pCommandStrings) {
        String lcResult = "Do not understand command";
        if (pCommandStrings.Length >= 2) {                      //if no# command strings greater than 1
			CommandMap lcCommandMap = new CommandMap ();
			if (lcCommandMap.RunCmd (pCommandStrings)) {        //if RunCmd method recognises a mapped command
                //Command lcCommand = new Command()
				lcResult = lcCommandMap.Result;                 //output string 'Result' of the mapped command

            }
            else                                                //if no mapped command is recognised
            {
                lcResult = DetermineSceneOutput() + "\n" + lcResult;     //else output string of current story? How about if we are in map or inventory?
            }
				
		} else // parts.Length < 2
        {
            lcResult = DetermineSceneOutput() + "\n" + lcResult;     //else output string of current story? How about if we are in map or inventory?
        }
        return lcResult;
	}

    public string DetermineSceneOutput()
    {
        string lcOutputText = "Dont understand!";
        ////determine output based on unity scene
        switch (GameManager.instance.currentUScene())
        {
            case "GameScene":
                lcOutputText = GameManager.instance.gameModel.currentScene.ToString();
                break;
            case "ItemScene":
                lcOutputText = GameManager.instance.gameModel.currentScene.searchForItems();
                break;
                //default:
                //    output.text = GameManager.instance.gameModel.currentScene.ToString();
                //    break;
        }
        return lcOutputText;
    }
}


