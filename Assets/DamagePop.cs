using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePop : MonoBehaviour {

    Character character;
    Text healthText;

	// Use this for initialization
	void Start () {
        character = GameObject.Find("Character").GetComponent<Character>();

        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.gameObject.GetComponent<Text>()) {
                healthText = child.gameObject.GetComponent<Text>();
            }
      }
        healthText.text = "-" + character.damage.ToString();

        Invoke("DestroySelf", 2f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.up * Time.deltaTime, Space.World);
	}

    void DestroySelf() {
        Destroy(gameObject);
    }
}
