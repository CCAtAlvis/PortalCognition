using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    public float velocity;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.collider.gameObject;
        if ("Player" == other.tag)
        {
            other.GetComponent<Rigidbody>().velocity += transform.forward * velocity;
        }
    }
}
