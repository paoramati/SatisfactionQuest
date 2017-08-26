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

	public String Parse(String pCmdStr){
		String strResult = "Do not understand";

		pCmdStr = pCmdStr.ToLower();
		String[] parts = pCmdStr.Split(' '); // tokenise the command
		// List<String> matches = Tokenise(pCmdStr);
		if (parts.Length >= 2) {
			String strCmd = parts [0] + " " + parts [1];
			CommandMap aMap = 	new CommandMap ();
			if (aMap.runCmd (strCmd)) {
				strResult = aMap.Result;
			} else  
				strResult = GameManager.instance.gameModel.currentLocation.Story + "\n" + strResult; 
		} else // parts.Length < 2
			strResult = GameManager.instance.gameModel.currentLocation.Story + "\n" + strResult;  


		/*
			// process the tokens
		switch( parts[0]){
		case "pick" : 
			if( parts[1] == "up") {
				Debug.Log("Got Pick up");
				strResult = "Got Pick up";

				if( parts.Length == 3){
					String param = parts[2];
				}// do pick up command

				// GameModel.Pickup();
			}
			break;
		case "go" : 
			switch( parts[1]) {
			case"north": 
				Debug.Log("Got go North");
				strResult = "Got Go North";
				break;
			case "south": 
				Debug.Log("Got go South");
				strResult = "Got Go South";
				break;
			default: 
				Debug.Log(" do not know how to go there"); 
				strResult = "Do not know how to go there";
				break;
			}// end switch
			break;
		default:
			Debug.Log("Do not understand");
			strResult = "Do not understand";
			break;

		}// end switch
		*/
		return strResult;

	}// Parse
}


