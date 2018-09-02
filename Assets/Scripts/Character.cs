using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public float dodgeSpeed;
    public bool isFacingRight = true;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (isFacingRight)
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);
        }
        else {
            transform.rotation = new Quaternion(0, 180, 0, 1);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Note>()) {
            print(collision.transform.position);
            Destroy(collision.gameObject);
        }
    }
}
