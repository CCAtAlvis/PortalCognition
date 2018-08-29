using UnityEngine;

public class RotatingEnemyController : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public Vector3 angularVelocity;

    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(angularVelocity * Time.fixedDeltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.collider.tag;
        Debug.Log(tag);

        if ("Player" == tag)
        {
            PlayerMovementController PMC = collision.collider.GetComponent<PlayerMovementController>();
            PMC.Destroy();
        }
    }
}
