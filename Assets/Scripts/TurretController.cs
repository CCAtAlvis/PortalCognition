using UnityEngine;
using UnityEngine.Networking;

public class TurretController : NetworkBehaviour
{
    public float range = 5f;
    public Transform PartToRotate;
    public float turnSpeed = 10f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Transform target;
    private float fireCountdown = 0f;
    private string playerTag = "Player";


    private void Start()
    {
        if (!isServer)
            return;

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        if (!isServer)
            return;

        if (target == null)
            return;

        Vector3 direction = target.position - transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookrotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
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

        EnemyBulletController bulletController = bulletGO.GetComponent<EnemyBulletController>();
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        bulletController.forward = direction.normalized;

        NetworkServer.Spawn(bulletGO);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
