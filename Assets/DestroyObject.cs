using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

    public Vector3 initialPosition;
    public Quaternion initialRotation;
    public Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        initialPosition = gameObject.transform.position;
        initialRotation = gameObject.transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if ("Constraints" == other.tag)
        {
            Debug.Log("hit trigger");
            rb.velocity = Vector3.zero;
            DestroyFunction();
        }
    }
    public void DestroyFunction()
    {
        rb.isKinematic = true;
        gameObject.SetActive(false);
        gameObject.transform.position = initialPosition;
        gameObject.transform.rotation = initialRotation;
        rb.velocity = Vector3.zero;
        gameObject.SetActive(true);
        rb.isKinematic = false;
    }
}
