﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {

//	public bool isValid(string prUsername){
//		return true;
//	}


	public void LoadScene(int prLevel){
		SceneManager.LoadScene (prLevel);
	}
}
