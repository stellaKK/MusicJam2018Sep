using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour {

    public GameObject singleNotes;
    public GameObject doubleNotes;
    public GameObject doublePressNotes;

    public void SpawnSingleNote() {
        GameObject note = Instantiate(singleNotes, transform.position, Quaternion.identity);
        note.transform.parent = this.transform;
    }

    public void SpawnDoubleNote() {
        GameObject note = Instantiate(doubleNotes, transform.position, Quaternion.identity);
        note.transform.parent = this.transform;
    }

    public void SpawnDoublePressNotes() {
        GameObject note = Instantiate(doublePressNotes, transform.position, Quaternion.identity);
        note.transform.parent = this.transform;
    }
}
