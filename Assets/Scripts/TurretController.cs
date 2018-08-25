using UnityEngine;
using UnityEngine.Networking;

public class TurretController : NetworkBehaviour
{
    [Header("GameManager")]
    public PortalGameManager PGM;

    [Header("Fire Properties")]
    public float range = 5f;
    public float turnSpeed = 10f;
    public float fireRate = 1f;

    [Space(20)]
    public Transform PartToRotate;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Transform target;
    private float fireCountdown = 0f;
    private string playerTag = "Player";

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 direction = target.position - transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookrotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
			if (CheckRayhit())
                Shoot();

            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private bool CheckRayhit()
    {
		if (!isServer)
			return false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, target.position, out hit, range))
        {
            string goTag = hit.transform.gameObject.tag;
            //Debug.Log(goTag);

            if ("Player" == goTag)
                return true;
        }

        return false;
    }

    private void UpdateTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestPlayer = null;

        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= shortestDistance)
            {
                shortestDistance = distanceToPlayer;
                nearestPlayer = player;
            }
        }

        if (nearestPlayer != null && shortestDistance <= range)
        {
            target = nearestPlayer.transform;
        }
        else
        {
            target = null;
        }
    }

    private void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.transform);
        bulletGO.transform.parent = null;
        EnemyBulletController bulletController = bulletGO.GetComponent<EnemyBulletController>();
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        bulletController.forward = direction.normalized;
        bulletController.PGM = PGM;
        NetworkServer.Spawn(bulletGO);
//		Debug.LogError ("message from server: direction: " + direction);
//		Debug.LogError ("message from server: forward: " + bulletController.forward);
		RpcSetBullet (bulletGO, direction);
    }

	[ClientRpc]
	void RpcSetBullet(GameObject _bulletGO, Vector3 _direction)
	{
		EnemyBulletController bulletController = _bulletGO.GetComponent<EnemyBulletController>();
		bulletController.forward = _direction.normalized;
		bulletController.PGM = PGM;
//		Debug.LogError ("message from client: direction: " + _direction);
//		Debug.LogError ("message from client: forward: " + bulletController.forward);
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
