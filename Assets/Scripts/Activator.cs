using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

    public KeyCode key;
    public GameObject note;
    public float thrustSpeed = 50f;

    private bool active = false; // check if note is inside activator
    private bool hasPressed = false; // check if use has pressed key
    private float pressDelay = 0.1f; // how long before Player can press again

    private GameObject character;
    private Character characterScript;
    private SpriteRenderer spriteRenderer;
    private Color color;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        character = GameObject.Find("Character");
        characterScript = character.GetComponent<Character>();
        color = spriteRenderer.color;
    }
	
	// Update is called once per frame
	void Update () {
        // Get User Input on every frame update
        if (Input.GetKeyDown(key) && !hasPressed)
        {
            Pressed();
            // Condition when User wants to move character left or right
            if (key == KeyCode.A){
                print("move left");
                character.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * characterScript.dodgeSpeed);
            }  else if (key == KeyCode.D) {
                character.GetComponent<Rigidbody2D>().AddForce(Vector2.right * characterScript.dodgeSpeed);
            }
            // Condition when User wants to press Activator to destroy notes
            else
            {
                if (active)
                {
                    Destroy(note);
                }
            }
            // Reset Player press action after delay
            Invoke("ResetPressed", pressDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // As note enters Trigger zone of Activator, Set NoteVariable to this note
        // Then set active as true
        active = true;
        if (collision.GetComponent<Note>())
        {
            note = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
        if (collision.GetComponent<Note>())
        {
            Destroy(collision.gameObject);
        }
    }

    void Pressed()
    {
        hasPressed = true;
        spriteRenderer.color = Color.black;
    }

    void ResetPressed()
    {
        hasPressed = false;
        spriteRenderer.color = color;
    }
}
