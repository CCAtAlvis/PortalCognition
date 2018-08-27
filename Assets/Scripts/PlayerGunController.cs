using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Box {
	public GameObject obj;
	public Rigidbody rigidbody;
}

[System.Serializable]
public class Bullet {
	public GameObject obj;
	public PortalBulletController controller;
	public Rigidbody rigidbody;
}

public class PlayerGunController : NetworkBehaviour
{
	public Bullet bullet;

	public Box box;

	public Transform spawnPoint;
	public float gravityGunRange = 10;
	public new Camera camera;

	public GameObject bulletPrefab;

	private bool isHoldingBox = false;
	private int playerID;

	private void Start()
	{
		bullet.obj = Instantiate (bulletPrefab);
		bullet.obj.SetActive (false);
		bullet.rigidbody = bullet.obj.GetComponent<Rigidbody> ();
		bullet.controller = bullet.obj.GetComponent<PortalBulletController> ();
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

	public void SetPlayerID(int _ID)
	{
		playerID = _ID;
	}

}
