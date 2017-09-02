using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour {

    Image backgroundImage;
    
	void Start () {

        backgroundImage = gameObject.GetComponent<Image>();

        backgroundImage.sprite = Resources.Load<Sprite>("sign3");

        if (backgroundImage == null)
        {
            print("BackgroundImage is null");
        }
    }

    public void ChangeBackground(string pImageName)
    {

        backgroundImage.sprite = Resources.Load<Sprite>(pImageName);



    }

}
