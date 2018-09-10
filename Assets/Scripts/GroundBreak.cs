using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundBreak : MonoBehaviour {

    public float FadeInTime;

    GameObject ground1;
    GameObject ground2;
    GameObject ground3;

    bool isBreaking1 = false;
    bool isBreaking2 = false;
    bool isBreaking3 = false;

    bool hasSelected = false;
    bool hasReseted = false;

    float startBreak;

    Color oldColor;

    // Use this for initialization
    void Start () {
        ground1 = this.transform.GetChild(0).gameObject;
        ground2 = this.transform.GetChild(1).gameObject;
        ground3 = this.transform.GetChild(2).gameObject;
        oldColor = ground1.GetComponent<SpriteRenderer>().color;
    }
	
	// Update is called once per frame
	void Update () {
        if (hasSelected == false)
        {
           hasSelected = true;
           SelectBlock();
        }

        if (isBreaking1)
        {
            BreaksBlock(ground1);
        }

        if (isBreaking2)
        {
            BreaksBlock(ground2);
        }

        if (isBreaking3)
        {
            BreaksBlock(ground3);
        }
    }

    void SelectBlock() {
        int i;
        i = Random.Range(0, 3);
        if (i == 0) {
            startBreak = Time.time;
            ground1.GetComponent<SpriteRenderer>().color = Color.red;
            isBreaking1 = true;
        }
        else if (i == 1)
        {
            startBreak = Time.time;
            ground2.GetComponent<SpriteRenderer>().color = Color.red;
            isBreaking2 = true;
        }
        else if (i == 2)
        {
            startBreak = Time.time;
            ground3.GetComponent<SpriteRenderer>().color = Color.red;
            isBreaking3 = true;
        }
        Invoke("ResetSelection", FadeInTime + 6f);
    }

    void ResetSelection() {
        hasSelected = false;
    }

    void BreaksBlock(GameObject block) {
        SpriteRenderer fadePanel = block.GetComponent<SpriteRenderer>();
        Color currentColor = fadePanel.color;

        if (Time.time - startBreak < FadeInTime)
        {
            float alphaChange = Time.deltaTime / FadeInTime;
            currentColor.a -= alphaChange;
            fadePanel.color = currentColor;
        } else {
            block.SetActive(false);
            isBreaking1 = false;
            isBreaking2 = false;
            isBreaking3 = false;
            Invoke("ResetBlock", FadeInTime);
        }
    }

    void ResetBlock() {
        ground1.GetComponent<SpriteRenderer>().color = oldColor;
        ground2.GetComponent<SpriteRenderer>().color = oldColor;
        ground3.GetComponent<SpriteRenderer>().color = oldColor;
        print("changing color to white");
        ground1.SetActive(true);
        ground2.SetActive(true);
        ground3.SetActive(true);
    }
}
