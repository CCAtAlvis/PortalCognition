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
        //bullet2 = Instantiate (bullet,transform.right,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        bullet1.transform.position += -bullet1.transform.up * Time.deltaTime;
        //bullet2.transform.position -= bullet2.transform.right*Time.deltaTime;
    }
}
