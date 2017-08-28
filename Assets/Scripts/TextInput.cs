using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {

	InputField input;
	InputField.SubmitEvent se;
	InputField.OnChangeEvent ce;
	public Text output;

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

            //if (GameManager.instance.activeCanvas.name == "GameCanvas")//GameManager.instance.currentUScene () == "TextIO")
            //    output.text = GameManager.instance.gameModel.currentScene.Story;
            //else if (GameManager.instance.activeCanvas.name == "ItemsCanvas") //(GameManager.instance.currentUScene () == "ItemsScene")
            //    output.text = GameManager.instance.gameModel.currentScene.allItems();

            output.text = GameManager.instance.gameModel.currentLocation.Story;
        }
	}

	private void SubmitInput(string arg0)
	{
		string currentText = output.text;

		CommandProcessor cmdProcessor = new CommandProcessor();

        //string[] lcInput = aCmd.ParseInput(arg0);

        output.text = cmdProcessor.ProcessInput(cmdProcessor.ParseInput(arg0));     //looks somewhat clumsy, but this utilises modularised parse method within the process input method
            

		input.text = "";
		input.ActivateInputField();



	}

	private void ChangeInput( string arg0)
	{
		Debug.Log(arg0);
	}
}
