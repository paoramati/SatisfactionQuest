using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene {
    
    public string story;
    public string question;
    public string sceneStatus;          //holds the status of the scene
    public int Answer;
    public Item item;
    public string backgroundImageName;


    public Scene North;
    public Scene South;
    public Scene East;
    public Scene West;
    public Scene Previous;


    public Scene(string pStory)
	{
		story = pStory;
	}

    public Scene(string pStory, string pImageName)
    {
        story = pStory;
        backgroundImageName = pImageName;

    }

    public override string ToString()
    {
        return story + "\n" + sceneStatus;
    }

    public void changeSceneBackground()
    {
        //BackgroundChanger = new ChangeImage();
        //BackgroundChanger.changeBackground(BackgroundImageName);

    }

    public string searchForItems()
    {
        string lcResult = "You can see: ";
        Item currentItem = item;
        if (currentItem == null)
        {
            lcResult = lcResult + "nothing here.";
        }
        else
        {
            while (currentItem != null)
            {
                lcResult = lcResult + "\n" + currentItem.itemName;
                currentItem = currentItem.nextItem;
            }
        }
        return lcResult;
    }
}

public class Item
{
    public string itemName;
    public string description;
    public string secretLetter;
    public string SecretQuestion;
    public bool itemFound = false;
    public Item nextItem;

    public Item(string pName, string pSecretLetter)
    {
        itemName = pName;
        secretLetter = pSecretLetter;
    }

    public Item(string pName)
    {
        itemName = pName;
    }


}
