using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    Rigidbody player;
	// Use this for initialization
	void Start () {
        player = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
            player.AddForce(transform.forward);
    }
}
