using UnityEngine;
using UnityEngine.Networking;

public class PlayerGunController : NetworkBehaviour
{
    [System.Serializable]
    public class Player
    {
        public Transform spawnPoint;
        public GameObject sp;
        public float gravityGunRange = 10;
        public Camera camera;
    }

    public struct Box
    {
        public GameObject obj;
        public Rigidbody rigidbody;
    }

    public struct Bullet
    {
        public GameObject obj;
        public PortalBulletController controller;
        public Rigidbody rigidbody;
    }

    public Player player;
    public GameObject bulletPrefab;

    private Bullet bullet;
    private Box box;
    private bool isHoldingBox = false;
    private int playerID;

    private void Start()
    {
        bullet.obj = Instantiate(bulletPrefab, player.spawnPoint);
        bullet.obj.transform.parent = null;
        bullet.obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        bullet.obj.SetActive(false);
        bullet.rigidbody = bullet.obj.GetComponent<Rigidbody>();
        bullet.controller = bullet.obj.GetComponent<PortalBulletController>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (!isHoldingBox)
            {
                //TODO: convert this to proper Server-Client network code
                bullet.obj.SetActive(false);
                bullet.rigidbody.isKinematic = true;
                bullet.obj.transform.position = player.spawnPoint.position;
                bullet.controller.forward = player.camera.transform.forward;
                bullet.controller.ResetObj();
                bullet.rigidbody.isKinematic = false;
                bullet.obj.SetActive(true);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (isHoldingBox)
            {
                ThrowBox();
                CmdThrowBox();
            }
            else
            {
                GravityGun(player.spawnPoint.position, player.spawnPoint.forward, player.camera.transform.forward);
                CmdGravityGun(player.spawnPoint.position, player.spawnPoint.forward, player.camera.transform.forward);
            }
        }

        if (box.obj != null)
        {
            box.obj.transform.position = player.spawnPoint.position + player.spawnPoint.forward * 0.2f - Vector3.up * 0.3f;
            //box.obj.transform.rotation = Quaternion.identity;
        }
    }

    [Command]
    private void CmdGravityGun(Vector3 _position, Vector3 _forward, Vector3 _direction)
    {
        GravityGun(_position, _forward, _direction);
    }

    private void GravityGun(Vector3 _position, Vector3 _forward, Vector3 _direction)
    {
        RaycastHit hit;

        if (Physics.Raycast(_position, _direction, out hit, player.gravityGunRange))
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
                box.obj.transform.position = _position + _forward * 0.2f - Vector3.up * 0.3f;
                box.obj.transform.localScale = new Vector3(2, 2, 2);
                Debug.Log(box);
                isHoldingBox = true;
            }
        }
    }

    [Command]
    private void CmdThrowBox()
    {
        ThrowBox();
    }

    private void ThrowBox()
    {
        //reset the rigidbody component of box
        box.obj.transform.parent = null;
        box.obj.transform.localScale = Vector3.one;
        box.rigidbody.isKinematic = false;
        box.rigidbody.useGravity = true;
        box.rigidbody.mass = 10000;

        box.obj = null;

        isHoldingBox = false;
    }

    public void SetPlayerID(int _ID)
    {
        playerID = _ID;
    }
}
