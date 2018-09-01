﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singlePlayerMovement : MonoBehaviour {

    public GameObject portalType;
    public GameObject portalBluePrefab;
    public GameObject portalRedPrefab;
    public GameObject portalBlue;
    public GameObject portalRed;
    public Transform playerTransform;
    public Rigidbody rb;
    public GameObject portalbullet;
    public GameObject portalbullet2;
    private PortalBulletController pbc;
    //private DestroyObject d;
	// Use this for initialization
	void Start () {
        //d = gameObject.GetComponent<DestroyObject>();
        //d.initialPosition = transform.position;
        //d.initialRotation = transform.rotation;
        //d.initialTransform.position = transform.position;
        //d.initialTransform.rotation = transform.rotation;
        portalBlue = Instantiate(portalBlue);
        portalBlue.SetActive(false);
        portalRed = Instantiate(portalRed);
        portalRed.SetActive(false);
        portalbullet = Instantiate(portalbullet);
        portalbullet.SetActive(false);
        portalbullet2 = Instantiate(portalbullet);
        portalbullet2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * 10);
        }
        if(Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * 10);
        }
        if(Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right*10);
        }
        if(Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right*10);
        }
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up*10);
        }
        if(Input.GetKey(KeyCode.Q))
        {
            // portalType = portalBlue;
            // GameObject g = Instantiate(portalbullet, playerTransform.position - playerTransform.right*5, Quaternion.identity);
            portalbullet.SetActive(false);
            portalbullet.transform.position = playerTransform.position + playerTransform.forward * 2;
            portalbullet.transform.rotation = Quaternion.identity;
            portalbullet.transform.forward = playerTransform.forward;
            portalbullet.SetActive(true);
            pbc = portalbullet.GetComponent<PortalBulletController>();
            pbc.portalPrefab = portalBlue;
            pbc.otherPortal = portalRed;
            pbc.ResetObj();
        }
        if(Input.GetKey(KeyCode.E))
        {
            //portalType = portalRed;
            //GameObject g = Instantiate(portalbullet, playerTransform.position + playerTransform.right * 5, Quaternion.identity);
            portalbullet2.SetActive(false);
            portalbullet2.transform.position = playerTransform.position + playerTransform.forward * 2;
            Debug.Log("position");
            Debug.Log(portalbullet2.transform.position);
            portalbullet2.transform.rotation = Quaternion.identity;
            portalbullet2.transform.forward = playerTransform.forward;
            Debug.Log("portal");
            Debug.Log(portalbullet2.transform.forward);
            portalbullet2.SetActive(true);
            pbc = portalbullet2.GetComponent<PortalBulletController>();
            pbc.portalPrefab = portalRed;
            pbc.otherPortal = portalBlue;
            pbc.ResetObj();
        }
	}
   /* void OnTriggerEnter(Collider other)
    {
        if("Constraints"==other.tag)
        {
            Debug.Log("hit trigger");
            rb.velocity = Vector3.zero;
            d.DestroyFunction();

        }
    }*/
}
