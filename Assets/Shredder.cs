using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

    private Character character;

    // Use this for initialization
    void Start () {
        character = GameObject.Find("Character").GetComponent<Character>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            character.characterHealth -= 200;
        }
    }
}
