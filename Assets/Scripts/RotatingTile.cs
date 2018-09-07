using UnityEngine;

public class RotatingTile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if ("Player" == collision.collider.tag || "Box" == collision.collider.tag)
        {
            collision.collider.transform.parent = transform;
        }
    }
}
