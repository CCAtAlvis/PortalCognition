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
		//rb.velocity = forward * speedMultiplier;
	}

    public void ResetObj(Vector3 _forward)
    {
		rb.isKinematic = true;
		forward = _forward;
        rb.velocity = _forward * speedMultiplier;
//		Debug.LogError("reset obj " + _forward + "    velocity: " + rb.velocity);
		timer = 0;
		rb.isKinematic = false;
    }

    private void Update()
    {
		timer += Time.deltaTime;

		if (timer >= ttl)
			gameObject.SetActive (false);

		transform.position += forward * Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
		Debug.Log ("spawning");
        Vector3 collisionPoint = other.contacts[0].point;
        Vector3 contactNormal = other.contacts[0].normal;

        Quaternion portalRotation = Quaternion.LookRotation(contactNormal, Vector3.up);
        //GameObject spawnedPortal = Instantiate(selfPortal, collisionPoint - forward.normalized * 1.01f, portalRotation);
        
		Debug.Log (selfPortal);
//		GameObject p = Instantiate (selfPortal, collisionPoint - forward.normalized * 1.01f, portalRotation);
		GameObject p = Instantiate (selfPortal);
		p.transform.parent = null;
		p.transform.position = collisionPoint - forward.normalized * .01f;
		p.transform.rotation = portalRotation;

//		selfPortal.transform.position = collisionPoint - forward.normalized * 1.01f;
//        selfPortal.transform.rotation = portalRotation;

        //pm.otherPortal = otherPortal;
        //selfPortal.SetActive(true);

        //Destroy(this.gameObject);
        //instead of destroying.. disable it..
        //TODO: same as PlayerGunController
        //make a permanant referance to portal
        //disable already created one 
        //and respwan it in new position
        //do the same in the manager script for Portal 
		Destroy (this.gameObject);
    }
}
