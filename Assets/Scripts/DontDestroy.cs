using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Reference: 'Live Training 24th November 2014 - Creating a Scene Selection Menu' - Unity video tutorial
 */

public class DontDestroy : MonoBehaviour {

	// Use this for initialization
	void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
