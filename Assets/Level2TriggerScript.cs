 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2TriggerScript : MonoBehaviour {

    public GameObject StartPortal;
    public Transform loc1SpawnPoint;
    public Transform loc2SpawnPoint;
    public Transform loc3SpawnPoint;
    LocationScript ls;
	// Use this for initialization
	void Start () {
        ls = StartPortal.GetComponent<LocationScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if("LocationTrigger" == other.gameObject.tag)
        {
            Debug.Log("LocationTrigger");
            LocationType lt = other.GetComponent<LocationType>();
            if(lt.type==1)
            {
                Debug.Log("setToLocation1");
                ls.destination = loc1SpawnPoint;
            }
            if(lt.type==2)
            {
                Debug.Log("location2");
                ls.destination = loc2SpawnPoint;
            }
            if(lt.type==3)
            {
                Debug.Log("location 3");
                ls.destination = loc3SpawnPoint;
            }
        }
    }
}
