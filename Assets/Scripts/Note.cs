using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

    public float timeToTravel = 2f;

    private GameObject character;
    private GameObject activatorUpArrow;
    private GameObject activatorLeftArrow;
    private GameObject activatorRightArrow;
    private Rigidbody2D rigidBody;

    private Vector3 startPosition, upEndPosition, leftEndPosition, rightEndPosition;
    Vector3 controlPosition;
    private float t;
    private float travelCounter = 0f;
    private float noteToCharacterOffset = 0f;

    private void Awake()
    {
        character = GameObject.Find("Character");
        rigidBody = GetComponent<Rigidbody2D>();
        activatorUpArrow = GameObject.Find("ActivatorUpArrow");
        activatorLeftArrow = GameObject.Find("ActivatorLeftArrow");
        activatorRightArrow = GameObject.Find("ActivatorRightArrow");
        travelCounter = 0;
    }

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        upEndPosition = activatorUpArrow.transform.position;
        leftEndPosition = activatorLeftArrow.transform.position;
        rightEndPosition = activatorRightArrow.transform.position;

        if (gameObject.tag == "Up")
        {
            t += Time.deltaTime / timeToTravel;
            transform.position = Vector3.Lerp(startPosition, upEndPosition, t);
        }
        else if (gameObject.tag == "Left")
        {
            t += Time.deltaTime / timeToTravel;

            controlPosition = startPosition + (leftEndPosition - startPosition) / 2 + Vector3.left * 5.0f;
            Vector2 m1 = Vector3.Lerp(startPosition, controlPosition, t);
            Vector2 m2 = Vector3.Lerp(controlPosition, leftEndPosition, t);
            transform.position = Vector2.Lerp(m1, m2, t);
        }
        else if (gameObject.tag == "Right")
        {
            t += Time.deltaTime / timeToTravel;

            controlPosition = startPosition + (rightEndPosition - startPosition) / 2 + Vector3.right * 5.0f;
            Vector2 m1 = Vector3.Lerp(startPosition, controlPosition, t);
            Vector2 m2 = Vector3.Lerp(controlPosition, rightEndPosition, t);
            transform.position = Vector2.Lerp(m1, m2, t);
        }
    }
}
