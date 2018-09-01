using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recorder : MonoBehaviour {

    private Text record;

	// Use this for initialization
	void Start () {
        record = FindObjectOfType<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setText(float timePosition)
    {
        record.text += "f, " + timePosition.ToString();
    }
}
