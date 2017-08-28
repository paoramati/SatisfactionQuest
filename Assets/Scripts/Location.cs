using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location {

	public Image BackgroundImage;

    public string Story;
    public string Question;
    //public string Answer;
    public int Answer;
    public Item locationItem;


    public Location North;
    public Location South;
    public Location East;
    public Location West;
    public Location Previous;


    public Location(string prStory)
	{
		Story = prStory;
	}


}

public class Item
{
    public string Description;
    public string SecretLetter;
    public string SecretQuestion;
    public bool Found;

    public Item(string pDescription, string pSecretLetter, bool pFound)
    {
        Description = pDescription;
        SecretLetter = pSecretLetter;
        Found = pFound;
    }
    
}
