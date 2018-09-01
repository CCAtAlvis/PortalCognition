using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour {
    public int vel;
	// Use this for initialization
	void Start () { 

	}
	
	// Update is called once per frame
	void Update () {

        transform.RotateAround(transform.position, Vector3.up, vel* Time.deltaTime);
	}
}
