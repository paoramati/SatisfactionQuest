using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    InputField input;
    InputField.SubmitEvent se;
    InputField.OnChangeEvent ce;
    CommandProcessor cmdProcessor;
    public Text output;
    public Image backgroundImage;
    public Text previousInput;

    // Use this for initialization
    void Start()
    {
        input = this.GetComponent<InputField>();
        if (input != null)
        { // if we get a null this script is running when it should not
            se = new InputField.SubmitEvent();
            se.AddListener(SubmitInput);
            input.onEndEdit = se;
            cmdProcessor = new CommandProcessor();
            output.text = cmdProcessor.GetSceneOutput();        //get output text for first game scene
            ChangeBackgroundImage();                            //get background image for first game scene
        }
    }

    private void SubmitInput(string arg0)
    {
        output.text = cmdProcessor.ProcessInput(cmdProcessor.ParseInput(arg0));
        ChangeBackgroundImage();
        input.text = "";
        input.ActivateInputField();
    }

    private void ChangeBackgroundImage()
    {
        if (GameManager.instance.GetCurrentScene() == "GameScene")
            backgroundImage.sprite = Resources.Load<Sprite>(GameManager.instance.gameModel.currentLocation.backgroundImageName);
    }

    private void ChangeInput(string arg0)
    {
        Debug.Log(arg0);
    }
}
