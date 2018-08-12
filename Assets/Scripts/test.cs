using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        Debug.Log("at i= all culling mask value = " + cam.cullingMask);
        cam.cullingMask = 0;
        for (int i = 0; i < 13; i++)
        {
            cam.cullingMask |= 1 << i;
            Debug.Log("at i= " + i + " culling mask value = " + cam.cullingMask);
        }
	}

    private void OnPreCull()
    {
        //Debug.Log(cam.cullingMask);
    }
}
