using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    //private Transform playerTarget;
    public Vector3 forward;
    public Rigidbody rg;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
        //    playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        //    targetVector = playerTarget.position - transform.position;
    }

    private void FixedUpdate()
    {
        rg.AddForce(forward*2, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
