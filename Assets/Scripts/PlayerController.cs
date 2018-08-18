using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerMesh;

    Rigidbody playerRb;
    new Camera camera;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        camera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        Quaternion camRotation = camera.transform.rotation;
        camRotation.x = 0;
        camRotation.z = 0;

        playerMesh.transform.rotation = camRotation;
    }

    private void FixedUpdate()
    {
        Vector3 camForward = camera.transform.forward;
        camForward.y = 0;

        if (Input.GetKey(KeyCode.W))
            playerRb.AddForce(camForward.normalized);
    }
}
