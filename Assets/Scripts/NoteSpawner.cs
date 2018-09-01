using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour {

    public GameObject noteToSpawn;

	// Use this for initialization
	void Start () {
        GameObject note = Instantiate(noteToSpawn, transform.position, Quaternion.identity);
        note.transform.parent = this.transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
