using UnityEngine;
using UnityEngine.Networking;

public class PlayerGunController : NetworkBehaviour
{
    [System.Serializable]
    public class Box
    {
        public GameObject obj;
        public Rigidbody rigidbody;
    }

    [System.Serializable]
    public class Bullet
    {
        public GameObject obj;
        public PortalBulletController controller;
        public Rigidbody rigidbody;
    }

    [System.Serializable]
    public class Player
    {
        public Transform spawnPoint;
        public float gravityGunRange = 10;
        public Camera camera;
    }

    public Player player;
    public GameObject bulletPrefab;

    private Bullet bullet;
    private Box box;
    private bool isHoldingBox = false;
    private int playerID;

    private void Start()
    {
        bullet.obj = Instantiate(bulletPrefab);
        bullet.obj.SetActive(false);
        bullet.rigidbody = bullet.obj.GetComponent<Rigidbody>();
        bullet.controller = bullet.obj.GetComponent<PortalBulletController>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("fire in the hole");
            if (isHoldingBox)
            {
                CmdDropBox();
            }
            else
            {
                CmdGravityGun();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isHoldingBox)
            {

                bullet.obj.SetActive(false);
                bullet.rigidbody.isKinematic = true;
                bullet.obj.transform.position = player.spawnPoint.position;
                bullet.controller.forward = player.camera.transform.forward;
                bullet.rigidbody.isKinematic = true;
                bullet.obj.SetActive(true);
            }
        }
    }

    [Command]
    private void CmdGravityGun()
    {
        RaycastHit hit;

        if (Physics.Raycast(player.spawnPoint.position, player.camera.transform.forward, out hit, player.gravityGunRange))
        {
            string goTag = hit.transform.gameObject.tag;
            Debug.Log(goTag);

            if ("Box" == goTag)
            {
                //this is it!
                //get the box
                box.obj = hit.transform.gameObject;
                box.rigidbody = box.obj.GetComponent<Rigidbody>();

                box.obj.transform.parent = player.spawnPoint.parent;
                box.rigidbody.useGravity = false;
                box.rigidbody.mass = 0.001f;
                //boxRb.position = spawnPoint.position;
                box.rigidbody.isKinematic = true;
                box.obj.transform.position = player.spawnPoint.position - player.spawnPoint.forward * 0.2f;
                box.obj.transform.localScale = new Vector3(2, 2, 2);

                isHoldingBox = true;
            }
        }
    }

    [Command]
    private void CmdDropBox()
    {
        //reset the rigidbody component of box
        box.obj.transform.parent = null;
        box.obj.transform.localScale = Vector3.one;
        box.rigidbody.isKinematic = false;
        box.rigidbody.useGravity = true;
        box.rigidbody.mass = 10000;

        isHoldingBox = false;
    }

    public void SetPlayerID(int _ID)
    {
        playerID = _ID;
    }
}
