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

    [SerializeField, HideInInspector]
    public Bullet bullet;
    private Box box;
    private bool isHoldingBox = false;

    [HideInInspector]
    public GameObject self;
    [HideInInspector]
    public GameObject other;
    public GameObject _bullet;
    private GameObject spawnedBullet;


	public GameObject blue;
	public GameObject red;

    private int playerID;
	private Rigidbody rb;

	private void Start() {
		rb = GetComponent<Rigidbody> ();
	}

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        //        if (Input.GetMouseButtonDown(0))
        if (Input.GetButtonDown("Portal"))
        {
            Debug.Log("firing portal");
            if (!isHoldingBox)
            {
                //TODO: convert this to proper Server-Client network code
                //FirePortal(player.spawnPoint.position, player.camera.transform.forward);
                //CmdFirePortal(player.spawnPoint.position, player.camera.transform.forward);
				CmdPortal(player.spawnPoint.position, player.camera.transform.forward + rb.velocity, playerID);
            }
        }

        //        if (Input.GetMouseButtonDown(1))
        if (Input.GetButtonDown("GravityGun"))
        {
            Debug.Log("gravity gun");
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
	private void CmdPortal(Vector3 _position, Vector3 _forward, int _id)
    {
        if (spawnedBullet != null)
            Destroy(spawnedBullet);

        GameObject bul = Instantiate(bulletPrefab);
        bul.transform.position = _position;
		NetworkServer.Spawn(bul);

		PortalBulletController pbc = bul.GetComponent<PortalBulletController>();

		if (1 == _id) {
			pbc.selfPortal = blue;
			pbc.otherPortal = red;
		} else {
			pbc.selfPortal = red;
			pbc.otherPortal = blue;
		}

		pbc.ResetObj(_forward);
		RpcPortal(bul, _forward, _id);
        spawnedBullet = bul;
    }

    [ClientRpc]
	private void RpcPortal(GameObject bul, Vector3 _forward, int _id)
    {
        PortalBulletController pbc = bul.GetComponent<PortalBulletController>();
        pbc.ResetObj(_forward);

		if (1 == _id) {
			pbc.selfPortal = blue;
			pbc.otherPortal = red;
		} else {
			pbc.selfPortal = red;
			pbc.otherPortal = blue;
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
            //Debug.Log(goTag);

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
                //Debug.Log(box);
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

	public void InitPlayer(int _id)
	{
		//Debug.LogError("in here");
		playerID = _id;

		PortalMechanism[] go = FindObjectsOfType<PortalMechanism> ();

		foreach(PortalMechanism j in go){
			Debug.LogError (j);
		}
	}
}
