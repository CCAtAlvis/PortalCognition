using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public GameObject playerMesh;
    public Transform spawnPoint;
    public float speedMultipler = 3;
    public float jumpMultiplier = 4;
    public float gravityGunRange = 10;
    public Rigidbody playerRb;
    public new Camera camera;
	public GameObject bulletPrefab;

    private bool isPlayerGrounded = false;
    private bool isHoldingBox = false;
    private GameObject boxGO;
    private Rigidbody boxRb;
	private int playerID;
	private GameObject bullet;
	private PortalBulletController pbc;
	private Rigidbody bulletRb;

    private void Start()
    {
		bullet = Instantiate (bulletPrefab);
		bullet.SetActive (false);
		bulletRb = bullet.GetComponent<Rigidbody> ();
		pbc = bullet.GetComponent<PortalBulletController> ();
        //playerRb = GetComponent<Rigidbody>();
        //camera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        Quaternion camRotation = camera.transform.rotation;
        camRotation.x = 0;
        camRotation.z = 0;

        playerMesh.transform.rotation = camRotation;

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

		if (Input.GetMouseButtonDown (0)) 
		{
			if (!isHoldingBox) {
				
				bullet.SetActive (false);
				bulletRb.isKinematic = true;
				bullet.transform.position = spawnPoint.position;
				pbc.forward = camera.transform.forward;
				bulletRb.isKinematic = true;
				bullet.SetActive (true);
			}
		}
    }

    [Command]
    private void CmdGravityGun()
    {
        RaycastHit hit;

        if (Physics.Raycast(spawnPoint.position, camera.transform.forward, out hit, gravityGunRange))
        {
            string goTag = hit.transform.gameObject.tag;
            Debug.Log(goTag);

            if ("Box" == goTag)
            {
                //this is it!
                //get the box
                boxGO = hit.transform.gameObject;
                boxRb = boxGO.GetComponent<Rigidbody>();

                boxGO.transform.parent = spawnPoint.parent;
                boxRb.useGravity = false;
                boxRb.mass = 0.001f;
                //boxRb.position = spawnPoint.position;
                boxRb.isKinematic = true;
                boxGO.transform.position = spawnPoint.position - spawnPoint.forward * 0.2f;
                boxGO.transform.localScale = new Vector3(2, 2, 2);

                isHoldingBox = true;
            }
        }
    }

    [Command]
    private void CmdDropBox()
    {
        //reset the rigidbody component of box
        boxGO.transform.parent = null;
        boxGO.transform.localScale = Vector3.one;
        boxRb.isKinematic = false;
        boxRb.useGravity = true;
        boxRb.mass = 10000;

        isHoldingBox = false;
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        Vector3 camForward = camera.transform.forward;
        camForward.y = 0;

        //Handle ~all~ movement game input after this
        if (!isPlayerGrounded)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            //playerRb.AddForce(camForward.normalized * forceMultiplier);
            //playerRb.AddForce(playerMesh.transform.forward * forceMultiplier);
            float temp = playerRb.velocity.y;
            Vector3 velocity = playerMesh.transform.forward * speedMultipler;
            playerRb.velocity = new Vector3(velocity.x, temp, velocity.z);
        }

        if (Input.GetKey(KeyCode.S))
        {
            float temp = playerRb.velocity.y;
            Vector3 velocity = -playerMesh.transform.forward * speedMultipler;
            playerRb.velocity = new Vector3(velocity.x, temp, velocity.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.velocity += Vector3.up * jumpMultiplier;
        }
    }

    private void SetPlayerGrounded(bool _set)
    {
        isPlayerGrounded = _set;

        if (_set)
        {
            playerRb.drag = 5;
            playerRb.useGravity = false;
        }
        else
        {
            playerRb.drag = 0.001f;
            playerRb.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.collider.gameObject.tag;
        if("Ground" == tag || "Box" == tag)
            SetPlayerGrounded(true);
    }

    private void OnCollisionStay(Collision collision)
    {
        string tag = collision.collider.gameObject.tag;
        if ("Ground" == tag || "Box" == tag)
            SetPlayerGrounded(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        string tag = collision.collider.gameObject.tag;
        if ("Ground" == tag || "Box" == tag)
            SetPlayerGrounded(false);
    }

	public void SetPlayerID(int _ID)
	{
		playerID = _ID;
	}
}
