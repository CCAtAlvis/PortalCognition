﻿using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public Rigidbody rb;
    public PortalGameManager pgm;

    [HideInInspector]
    public Vector3 initialPosition;
    [HideInInspector]
    public Quaternion initialRotation;

    private int index;
    private CheckpointScript cs;

    void Start()
    {
        if (tag == "Player")
            pgm = FindObjectOfType<PortalGameManager>();

        index = 0;
        initialPosition = gameObject.transform.position;
        initialRotation = gameObject.transform.rotation;
    }

    public void ChangeInitialPosition(Vector3 newPosition, Quaternion newRotation)
    {
        initialPosition = newPosition;
        initialRotation = newRotation;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered");
        if ("Constraints" == other.tag)
        {
            Debug.Log("hit trigger");
            DestroyFunction();
        }
        if ("Checkpoint" == other.tag && "Player" == gameObject.tag)
        {
            //Debug.Log("Checkpoint triggered by player");
            cs = other.gameObject.GetComponent<CheckpointScript>();
            if (cs.index > this.index)
            {
                this.index = cs.index;
                ChangeInitialPosition(other.transform.position, other.transform.rotation);
                pgm.checkpointsUI.text = "Checkpoint: " + index.ToString();
            }
        }
    }

    public void DestroyFunction()
    {
        rb.isKinematic = true;
        gameObject.SetActive(false);
        gameObject.transform.parent = null;
        gameObject.transform.position = initialPosition;
        gameObject.transform.rotation = initialRotation;
        rb.velocity = Vector3.zero;
        gameObject.SetActive(true);
        rb.isKinematic = false;
    }
}
