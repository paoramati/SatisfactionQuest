using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameModel
{
		
	public Scene currentScene;
	public Scene firstScene;

	private void makeStory()
	{
		Scene lcScene;

		firstScene = new Scene ("You are in a forest, it is dark. To the north is a castle. To the east is a swamp.");
		firstScene.North = new Scene ("You at the Castle. To the south is the forest.");
		firstScene.North.South = firstScene;

		firstScene.East = new Scene ("You in a swamp. To the west is a forest.");
		firstScene.East.West = firstScene;

		lcScene = new Scene("You in a dungeon. ");



		// Add more scenes here
		currentScene = firstScene;
	}
	public GameModel ()
	{
		makeStory ();
	}
}


