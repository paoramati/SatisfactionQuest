using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMap : MonoBehaviour {

	private Dictionary<string, Command> commands;
	public string Result = "";

	// Use this for initialization
	public CommandMap () {
		commands = new Dictionary<string,Command>();
		commands.Add("go north",new GoCommand("north"));

		commands.Add("go south",new GoCommand("south"));
		commands.Add("go east",new GoCommand("east"));
		commands.Add("go west",new GoCommand("west"));
		commands.Add("pick up", new PickCommand("up"));
	}
	
	public bool runCmd(string pStrCommand){
		bool lcResult = false;
		Command aCmd ;
		if (commands.ContainsKey(pStrCommand)) { 
			aCmd = commands [pStrCommand];
			aCmd.Do ();
			lcResult = true;
			// Get the current scene description
			Result = GameManager.instance.gameModel.currentScene.Story;
		} else {
			Debug.Log ("I do not understand"); 
			lcResult = false;
		}
		return lcResult;
	}
}
