using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scene
{
	public Scene (string pStory)
	{
		Story = pStory;
	}

	public string Story;

	public Scene North;
	public Scene East;
	public Scene South;
	public Scene West;
}

