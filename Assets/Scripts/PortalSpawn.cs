using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawn : MonoBehaviour
{
    public GameObject bullet;
    GameObject bullet1;
    GameObject bullet2;

    // Use this for initialization
    void Start()
    {
        bullet1 = Instantiate(bullet, transform.forward, Quaternion.identity);
        bullet2 = Instantiate(bullet, transform.right, Quaternion.identity);

        bullet1.GetComponent<BulletController>().forward = -bullet1.transform.up;
        bullet2.GetComponent<BulletController>().forward = -bullet2.transform.right;
    }
}
