using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject portalPrefab;
    public Vector3 forward;

    // Update is called once per frame
    void Update()
    {
        transform.position += forward * Time.deltaTime;
    }


    void OnCollisionEnter(Collision other)
    {
        Vector3 collisionPoint = other.contacts[0].point;
        Vector3 contactNormal = other.contacts[0].normal;

        Quaternion portalRotation = Quaternion.LookRotation(contactNormal, Vector3.up);
        GameObject spawnedPortal = Instantiate(portalPrefab, collisionPoint, portalRotation);

        Destroy(this.gameObject);
    }
}
