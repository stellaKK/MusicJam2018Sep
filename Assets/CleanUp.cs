using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviour {

    GameObject[] gameObjects ;

	// Use this for initialization
	void Start () {
        gameObjects = GameObject.FindGameObjectsWithTag("SoundPlayer");

        for (var i = 0; i < gameObjects.Length; i++) {
            Destroy(gameObjects[i]);
        }
    }
	
}
