using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMap
{ 

    private Dictionary<string, Command> commands;
    private string[] inputCommand;

    public string Result = "";

    // Use this for initialization
    public CommandMap()
    {
        commands = new Dictionary<string, Command>();
        //original commands
        ////commands.Add("go north", new GoCommand("north"));
        ////commands.Add("go south", new GoCommand("south"));
        ////commands.Add("go east", new GoCommand("east"));
        ////commands.Add("go west", new GoCommand("west"));
        //commands.Add("pick up", new PickCommand("up"));
        //commands.Add("show items", new ShowCommand("items"));
        //commands.Add("show scene", new ShowCommand("scene"));

        //granular commands
        //commands.Add("answer", new AnswerCommand());
        //commands.Add("go", new GoCommand());
        //commands.Add("pick", new PickCommand());
        //commands.Add("show", new ShowCommand());
        //commands.Add("read", new ReadCommand());



        //commands.Add("north", new GoCommand("north"));
    }

    public bool RunCmd(string[] pCommandStrings)
    {
        string uSceneName = GameManager.instance.currentUScene();               //gets current Unity scene
        bool lcResult = false;

        UpdateCommandMap(pCommandStrings);
        Command aCmd;
        if (commands.ContainsKey(pCommandStrings[0]))
        {
            aCmd = commands[pCommandStrings[0]];
            aCmd.Do(pCommandStrings);
            //aCmd.Do(this); // changes Result by side-effect
            lcResult = true;
            // Get the current scene description + other details. Can also add output such as "do not understand"
            //Result = GameManager.instance.gameModel.currentScene.Story + "\n";
            //Result += GameManager.instance.gameModel.currentScene.Question;

            /*
             * Insert a conditional here to determine what uScene we are in, and therefore, what kind of Result we will be fetching
             * Otherwise, make the gameModel return value not be tied to location scene, and get that value returned generically
             * 
             * When an item is picked up, this can be reflected in SceneStatus
             * 
             */

            switch (GameManager.instance.currentUScene())
            {
                case "GameScene":
                    Result = GameManager.instance.gameModel.currentScene.ToString();
                    //GameManager.instance.gameModel.currentScene.changeSceneBackground();

                    break;
                case "ItemScene":
                    Result = GameManager.instance.gameModel.currentScene.searchForItems();

                    break;
                case "MapScene":
                    break;
                case "MenuScene":
                    break;
                
            }


        }
        else
        {
            Result = "I do not understand.";
            Debug.Log("I do not understand");
            lcResult = false;
        }
        return lcResult;
    }

    private void UpdateCommandMap(string[] pCommandStrings)
    {
        //string[] lcInputCommand = (string[])pCommandStrings.Clone();
        //inputCommand = (string[])pCommandStrings.Clone();

        commands.Add("answer", new AnswerCommand(pCommandStrings));
        commands.Add("go", new GoCommand(pCommandStrings));
        commands.Add("pick", new PickCommand(pCommandStrings));
        commands.Add("show", new ShowCommand(pCommandStrings));
        commands.Add("read", new ReadCommand(pCommandStrings));



    }
}
