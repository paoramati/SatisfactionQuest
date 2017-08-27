using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location {

	public Image BackgroundImage;

    public string Story;

    public Location North;
    public Location South;
    public Location East;
    public Location West;
    public Location Previous;

    public string Question;
    //public string Answer;
    public int Answer;

    public Location(string prStory)
	{
		Story = prStory;
	}


}
