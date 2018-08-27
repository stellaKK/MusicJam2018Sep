using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.Translate(Vector3.down * 10f * Time.deltaTime, Space.World);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
