using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScript : MonoBehaviour {

    public Transform destination;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if("Player"==other.tag)
        {
            Debug.Log("Playerhit");
            other.transform.position = destination.position;
            other.transform.rotation = destination.rotation;
        }
    }
}
