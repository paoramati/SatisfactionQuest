using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour {

    Image BackgroundImage;
    
    //public Sprite BackgroundSprite;
    

    //Image[] images;

	// Use this for initialization
	void Start () {

        BackgroundImage = gameObject.GetComponent<Image>();

        BackgroundImage.sprite = Resources.Load<Sprite>("beach1");
        //gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Backgrounds/beach1");

        //images = gameObject.GetComponent<Image>();
        //images = gameObject.GetComponentInChildren<Image>();
        //BackgroundImage = gameObject.GetComponent<Image>();

        //BackgroundImage.sprite = BackgroundSprite;

        //foreach (Image image in images)
        //{
        //    image.sprite = BackgroundSprite;
        //}

        //BackgroundImage.gameObject.ge

        if (BackgroundImage == null)
        {
            print("BackgroundImage is null");
        }
    }

    //// Update is called once per frame
    //void Update () {

    //}

    public void changeBackground(string pImageName)
    {

        BackgroundImage.sprite = Resources.Load<Sprite>(pImageName);
        //var backgroundImage = GameManager.instance.gameObject.GetComponent<Image>();
        //GetComponent<Image>().sprite = Resources.Load("Images/Backgrounds/" + imageName);
        //backgroundImage = Resources.Load("Images/Backgrounds/" + imageName);
    }

}
