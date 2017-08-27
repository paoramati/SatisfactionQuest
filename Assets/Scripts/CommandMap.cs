using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMap
{ 

    private Dictionary<string, Command> commands;
    public string Result = "";

    // Use this for initialization
    public CommandMap()
    {
        commands = new Dictionary<string, Command>();
        //original commands
        commands.Add("go north", new GoCommand("north"));
        commands.Add("go south", new GoCommand("south"));
        commands.Add("go east", new GoCommand("east"));
        commands.Add("go west", new GoCommand("west"));
        commands.Add("pick up", new PickCommand("up"));
        commands.Add("show items", new ShowCommand("items"));
        commands.Add("show location", new ShowCommand("location"));

        //granular commands
        commands.Add("answer", new AnswerCommand());
        commands.Add("go", new GoCommand());
        commands.Add("pick", new PickCommand());
        commands.Add("show", new ShowCommand());
        commands.Add("read", new ReadCommand());
    }

    public bool runCmd(string[] pCommandStrings)
    {
        bool lcResult = false;
        Command aCmd;
        if (commands.ContainsKey(pCommandStrings[0]))
        {
            aCmd = commands[pCommandStrings[0]];
            aCmd.Do(pCommandStrings);
            //aCmd.Do(this); // changes Result by side-effect
            lcResult = true;
            // Get the current scene description
            Result = GameManager.instance.gameModel.currentLocation.Story;
        }
        else
        {
            Debug.Log("I do not understand");
            lcResult = false;
        }
        return lcResult;
    }
}
