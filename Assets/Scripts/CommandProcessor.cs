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
        if (pCommandStrings.Length >= 2) {        //2nd condition for answer command
            
			//String lcCommand = pCommandStrings [0] + " " + pCommandStrings [1];     //only works for commands with 2 words
			CommandMap lcCommandMap = new CommandMap ();
			if (lcCommandMap.runCmd (pCommandStrings)) {
                //Command lcCommand = new Command()
				lcResult = lcCommandMap.Result;
			} else  
				lcResult = GameManager.instance.gameModel.currentLocation.Story + "\n" + lcResult; 
		} else // parts.Length < 2
			lcResult = GameManager.instance.gameModel.currentLocation.Story + "\n" + lcResult;  

		return lcResult;

	}
}


