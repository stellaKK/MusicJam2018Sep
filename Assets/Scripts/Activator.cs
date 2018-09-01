using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

    public KeyCode key;
    public GameObject note;
    private bool active = false;
    private bool hasPressed = false;
    private SpriteRenderer spriteRenderer;
    private Color color;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(key) && !hasPressed)
        {
            Pressed();
            if (active)
            {
                Destroy(note);
            }
            Invoke("ResetPressed", 0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        active = true;
        print(collision.name);
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
