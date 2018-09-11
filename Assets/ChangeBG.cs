using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBG : MonoBehaviour {

    public bool isQuest3Completed = false;
    public GameObject backGround1;
    public GameObject backGround2;

    // Use this for initialization
    void Start () {
        backGround1 = GameObject.Find("BackGround1");
        backGround2 = GameObject.Find("BackGround2");

        if (PlayerPrefs.GetInt("Quest3Completed") == 1)
        {
            isQuest3Completed = true;
        }
        else {
            isQuest3Completed = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isQuest3Completed == true)
        {
            backGround1.SetActive(false);
            backGround2.SetActive(true);
        }
        else {
            backGround1.SetActive(true);
            backGround2.SetActive(false);
        }
	}
}
