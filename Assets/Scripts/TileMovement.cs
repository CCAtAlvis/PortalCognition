using UnityEngine;

public class TileMovement : MonoBehaviour
{
    public int vel;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, vel * Time.deltaTime);
    }
}
