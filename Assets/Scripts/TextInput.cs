using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {

	InputField input;
	InputField.SubmitEvent se;
	InputField.OnChangeEvent ce;
	public Text output;
    CommandProcessor cmdProcessor;


    //Sprite backgroundImage;

    // Use this for initialization
    void Start () {
		input = this.GetComponent<InputField>();
		if (input != null) { // if we get a null this script is running when it should not
			se = new InputField.SubmitEvent ();
			se.AddListener (SubmitInput);
			/*
		    ce = new InputField.OnChangeEvent();
		    ce.AddListener(ChangeInput);
		   */
			input.onEndEdit = se;
            //input.onValueChanged = ce;

            cmdProcessor = new CommandProcessor();

            output.text = cmdProcessor.DetermineSceneOutput();
            ////determine output based on unity scene
            //switch (GameManager.instance.currentUScene())
            //{
            //    case "GameScene":
            //        output.text = GameManager.instance.gameModel.currentScene.ToString();
            //        break;
            //    case "ItemScene":
            //        output.text = GameManager.instance.gameModel.currentScene.searchForItems();
            //        break;
            //        //default:
            //        //    output.text = GameManager.instance.gameModel.currentScene.ToString();
            //        //    break;
            //}
        }
	}

	private void SubmitInput(string arg0)
	{
		string currentText = output.text;

		//CommandProcessor cmdProcessor = new CommandProcessor();

        output.text = cmdProcessor.ProcessInput(cmdProcessor.ParseInput(arg0));     //looks somewhat clumsy, but this utilises modularised parse method within the process input method

		input.text = "";
		input.ActivateInputField();
	}

	private void ChangeInput( string arg0)
	{
		Debug.Log(arg0);
	}
}
