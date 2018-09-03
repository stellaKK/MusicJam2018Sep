using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activators : MonoBehaviour {

    private GameObject character;

    // Use this for initialization
    void Start()
    {
        character = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector2(character.transform.position.x, transform.position.y);
    }
}
