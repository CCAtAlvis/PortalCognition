using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Vector3 collisionPoint;
    Vector3 contactNormal;
    public GameObject portal;
    GameObject object1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision Occured");
        collisionPoint = other.contacts[0].point;
        contactNormal = other.contacts[0].normal;
        Debug.Log(contactNormal);
        Quaternion portalRotation = Quaternion.LookRotation(contactNormal, Vector3.up);
        object1 = Instantiate(portal, collisionPoint, portalRotation);
        Transform[] tra = object1.GetComponentsInChildren<Transform>();
        tra[2].rotation = portalRotation;
        //ChangeTransformRecursively (object1.transform,portalRotation);
        Debug.Log(object1.transform.forward);
    }

    public void ChangeTransformRecursively(Transform _trans, Quaternion _rot)
    {
        _trans.rotation = _rot;
        Debug.Log(_trans.rotation);

        foreach (Transform child in _trans)
        {
            ChangeTransformRecursively(child, _rot);
        }
    }
}
