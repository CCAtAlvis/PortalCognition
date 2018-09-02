using UnityEngine;

public class PortalBulletController : MonoBehaviour
{
    public GameObject selfPortal;
    public GameObject otherPortal;

    public int speedMultiplier = 6;
    public float ttl;

    [HideInInspector]
    public Vector3 forward;
    
	[SerializeField]
    private Rigidbody rb;
	private float timer;

	//public GameObject portalToSpawn;

	private void Start()
	{
        //Debug.LogError("starting bullet forward: " + forward);
		rb.velocity = forward * speedMultiplier;
	}

    public void ResetObj(Vector3 _forward)
    {
        Debug.LogError("reset obj " + _forward);
        Debug.LogError(_forward);
        rb.velocity = _forward * speedMultiplier;
		timer = 0;
    }

    private void Update()
    {
		timer += Time.deltaTime;

		if (timer >= ttl)
			gameObject.SetActive (false);
    }

    void OnCollisionEnter(Collision other)
    {
        Vector3 collisionPoint = other.contacts[0].point;
        Vector3 contactNormal = other.contacts[0].normal;

        Quaternion portalRotation = Quaternion.LookRotation(contactNormal, Vector3.up);
        //GameObject spawnedPortal = Instantiate(selfPortal, collisionPoint - forward.normalized * 1.01f, portalRotation);
        selfPortal.transform.position = collisionPoint - forward.normalized * 1.01f;
        selfPortal.transform.rotation = portalRotation;
        PortalMechanism pm = selfPortal.GetComponent<PortalMechanism>();
        pm.otherPortal = otherPortal;
        selfPortal.SetActive(true);
        //Destroy(this.gameObject);
        //instead of destroying.. disable it..
        //TODO: same as PlayerGunController
        //make a permanant referance to portal
        //disable already created one 
        //and respwan it in new position
        //do the same in the manager script for Portal 
        gameObject.SetActive(false);
    }
}
