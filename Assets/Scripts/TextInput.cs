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
    public Text lblEsteemOutput;

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


            if (GameManager.GetCurrentScene() == "GameScene")   //initialise background camera if game scene
            {
                WebCamDevice[] devices = WebCamTexture.devices;                 //capture all connected cameras
                WebCamTexture texture = new WebCamTexture(devices[1].name);     //set texture to front camera    
                backgroundImage.material.mainTexture = texture;
                texture.Play();                                                 //play recording
            }

            ChangeEsteemOutput();
        }
    }

    private void SubmitInput(string arg0)
    {
        output.text = cmdProcessor.ProcessInput(cmdProcessor.ParseInput(arg0));
        ChangeBackgroundCameraImage();
        input.text = "";
    }

    //display background image
    private void ChangeBackgroundImage()
    {
        if (GameManager.GetCurrentScene() == "GameScene")
            backgroundImage.sprite = Resources.Load<Sprite>(GameManager.instance.gameModel.currentLocation.background);
    }

    private void ChangeBackgroundCameraImage()
    {
        if (GameManager.GetCurrentScene() == "GameScene")
        {
            WebCamDevice[] devices = WebCamTexture.devices;                 //capture all connected cameras
            WebCamTexture texture = new WebCamTexture(devices[1].name);     //set texture to back camera    
            backgroundImage.material.mainTexture = texture;
            texture.Play();                                                 //play background recording
        }
    }

    private void ChangeEsteemOutput()
    {
        if (GameManager.GetCurrentScene() == "GameScene")
            lblEsteemOutput.text = GameManager.instance.Player1.esteem.ToString();  //display player esteem
    }

    private void ChangeInput(string arg0)
    {
        Debug.Log(arg0);
    }
}
