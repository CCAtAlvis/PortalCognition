﻿using UnityEngine;

public class PortalMechanism : MonoBehaviour
{
    public GameObject otherPortal;

    [HideInInspector]
    public GameObject incomingPlayer;

    private GameObject player;
    private Rigidbody playerrb;

    public float ttl;
    private float timer;

    // Use this for initialization
    void Start()
    {
        incomingPlayer = null;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if(timer>ttl)
        {
            timer = 0;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Triggered");
            player = other.gameObject;

            if (incomingPlayer == player)
            {
                Debug.Log("same incoming player");
                return;
            }
            if (otherPortal.activeSelf == false)
                return;

            PortalMechanism pm = otherPortal.GetComponent<PortalMechanism>();
            pm.incomingPlayer = player;
            playerrb = player.GetComponent<Rigidbody>();
            
			player.SetActive(false);
            Vector3 vel = playerrb.velocity;
            playerrb.isKinematic = true;
            float velMagnitute = vel.magnitude;
            
			//Debug.Log(velMagnitute);
            player.transform.position = otherPortal.transform.position;
            //Debug.Log(player.transform.position);
            //player.transform.forward = otherPortal.transform.forward;
			player.SetActive(true);
            playerrb.isKinematic = false;
            playerrb.velocity = otherPortal.transform.forward * velMagnitute;
            //Debug.Log(playerrb.velocity.magnitude);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            return;

        incomingPlayer = null;
        gameObject.SetActive(false);
        otherPortal.SetActive(false);
    }
}
