//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CommandMap
//{ 
//    private Dictionary<string, Command> commands;
//    private string[] inputCommand;
//    public string Result = "";

//    // Use this for initialization
//    public CommandMap()
//    {
//        commands = new Dictionary<string, Command>();
//    }

//    public bool RunCmd(string[] pCommandStrings)
//    {
//        string uSceneName = GameManager.instance.currentUScene();               //gets current Unity scene
//        bool lcResult = false;

//        UpdateCommandMap(pCommandStrings);
//        Command aCmd;
//        if (commands.ContainsKey(pCommandStrings[0]))
//        {
//            aCmd = commands[pCommandStrings[0]];
//            aCmd.Do(pCommandStrings);                   //do the mapped command
//            lcResult = true;


//            /*
//             * Insert a conditional here to determine what uScene we are in, and therefore, what kind of Result we will be fetching
//             * Otherwise, make the gameModel return value not be tied to location scene, and get that value returned generically
//             * 
//             * When an item is picked up, this can be reflected in SceneStatus
//             * 
//             */
            
//            //...then this structure determines what Result will be. But why? The command will return a result
//            switch (GameManager.instance.currentUScene())
//            {
//                case "GameScene":
//                    Result = GameManager.instance.gameModel.currentScene.ToString();
//                    //GameManager.instance.gameModel.currentScene.changeSceneBackground();

//                    break;
//                case "ItemScene":
//                    Result = GameManager.instance.gameModel.currentScene.searchForItems();

//                    break;
//                case "MapScene":
//                    break;
//                case "MenuScene":
//                    break;
                
//            }


//        }
//        else
//        {
//            Result = "I do not understand.";
//            Debug.Log("I do not understand");
//            lcResult = false;
//        }
//        return lcResult;
//    }

//    private void UpdateCommandMap(string[] pCommandStrings)
//    {
//        //string[] lcInputCommand = (string[])pCommandStrings.Clone();
//        //inputCommand = (string[])pCommandStrings.Clone();

//        commands.Add("answer", new AnswerCommand(pCommandStrings));
//        commands.Add("go", new GoCommand(pCommandStrings));
//        commands.Add("pick", new PickCommand(pCommandStrings));
//        commands.Add("show", new ShowCommand(pCommandStrings));
//        commands.Add("read", new ReadCommand(pCommandStrings));



//    }
//}
