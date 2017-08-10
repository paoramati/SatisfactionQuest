using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : MonoBehaviour {

	private Command next;
	// Use this for initialization
	void Awake () {
		
	}

	public virtual void Do(){
	}


}

public class GoCommand : Command {
	private string adverb;

	public GoCommand( string pAdverb)
	{
		adverb = pAdverb;
	}

	public override void Do(){
		Debug.Log ("Got a Go" + adverb);

		Scene aScene = GameManager.instance.gameModel.currentScene;

		switch (adverb) 
		{
		case "north":
			aScene = GameManager.instance.gameModel.currentScene;
			if(aScene.North != null)
				GameManager.instance.gameModel.currentScene = aScene.North;
			break;
		case "south":
			aScene = GameManager.instance.gameModel.currentScene;
			if(aScene.South != null)
				GameManager.instance.gameModel.currentScene = aScene.South;
			break;
		case "east":
			aScene = GameManager.instance.gameModel.currentScene;
			if(aScene.East != null)
				GameManager.instance.gameModel.currentScene = aScene.East;
			break;
		case "west":
			aScene = GameManager.instance.gameModel.currentScene;
			if(aScene.West != null)
				GameManager.instance.gameModel.currentScene = aScene.West;
			break;
		}
	}
}

public class PickCommand : Command {
	private string adverb;

	public PickCommand( string pAdverb)
	{
		adverb = pAdverb;
	}

	public override void Do(){
		Debug.Log ("Got a Pick" + adverb);

	}
}

