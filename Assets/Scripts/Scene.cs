using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene {

    public string locationName;
    public string story;
    public string question;
    public string sceneStatus;          //holds the status of the scene
    public int Answer;
    public Item item;
    public string backgroundImageName;
    public int sceneNumber;
    public int sceneCounter = 0;


    public Scene North;
    public Scene South;
    public Scene East;
    public Scene West;
    public Scene Previous;

    //public Scene(string pStory)
    //{
    //    story = pStory;
    //}

    public Scene(string pLocationName)
    {
        locationName = pLocationName;
    }

    public Scene(string pLocationName, string pStory)
	{
        locationName = pLocationName;
		story = pStory;
	}

    public Scene(string pLocationName, string pStory, string pImageName)
    {
        locationName = pLocationName;
        story = pStory;
        backgroundImageName = pImageName;
    }

    public override string ToString()
    {
        return story + "\n" + sceneStatus;
    }

    public string DisplaySceneDetails()
    {
        string lcResult = "";
        lcResult = locationName + ":\n";

        if (this.North != null)
            lcResult += "To the North is a " + this.North.locationName + "\n";
        if (this.South != null)
            lcResult += "To the South is a " + this.South.locationName + "\n";
        if (this.East != null)
            lcResult += "To the East is a " + this.East.locationName + "\n";
        if (this.West != null)
            lcResult += "To the West is a " + this.West.locationName + "\n";


        lcResult += sceneStatus;
        return lcResult;
    }

    public void changeSceneBackground()
    {
        //BackgroundChanger = new ChangeImage();
        //BackgroundChanger.changeBackground(BackgroundImageName);

    }

    public string SearchForItems()
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
