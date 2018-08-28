using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    //private Transform playerTarget;
	private int playerID;
    public Vector3 forward;
	public int speedMultiplier = 6;
    public Rigidbody rg;
    public float ttl = 2f;

    public PortalGameManager PGM;

    private void Start()
    {
		Debug.Log (forward);
		rg.velocity = forward * speedMultiplier;
    }

    private void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0)
            Destroy(this.gameObject);
    }

//    private void FixedUpdate()
//    {
//        rg.AddForce(forward, ForceMode.VelocityChange);
//    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Player" == other.tag)
        {
            Debug.Log("player hit adding 10sec to time.");
            PGM.AddTime(10f);
        }

        Destroy(this.gameObject);
    }

	public void Destroy(){
	}
}
