using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersScript : MonoBehaviour {

    //public Material defaultMaterial;
    public int index;
	// Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if("Player" == other.tag)
            gameObject.GetComponentInParent<MultitriggerScript>().ChildTriggered(index,gameObject);

    }
}
