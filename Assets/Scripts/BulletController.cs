using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject portalPrefab;
    public Vector3 forward;

    [SerializeField]
    private Rigidbody rb;

    void Update()
    {
        rb.AddForce(forward, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {
        Vector3 collisionPoint = other.contacts[0].point;
        Vector3 contactNormal = other.contacts[0].normal;

        Quaternion portalRotation = Quaternion.LookRotation(contactNormal, Vector3.up);
        GameObject spawnedPortal = Instantiate(portalPrefab, collisionPoint - forward.normalized * 1.01f, portalRotation);

        Destroy(this.gameObject);
    }
}
