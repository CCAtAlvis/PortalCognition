using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerMesh;
    public float speedMultipler = 3;
    public float jumpMultiplier = 4;

    private Rigidbody playerRb;
    private new Camera camera;
    private bool isPlayerGrounded = false;

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

        //Handle all game input here
        if (!isPlayerGrounded)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            //playerRb.AddForce(camForward.normalized * forceMultiplier);
            //playerRb.AddForce(playerMesh.transform.forward * forceMultiplier);
            float temp = playerRb.velocity.y;
            Vector3 velocity = playerMesh.transform.forward * speedMultipler;
            playerRb.velocity = new Vector3(velocity.x, temp, velocity.z);
        }

        if (Input.GetKey(KeyCode.S))
        {
            float temp = playerRb.velocity.y;
            Vector3 velocity = -playerMesh.transform.forward * speedMultipler;
            playerRb.velocity = new Vector3(velocity.x, temp, velocity.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.velocity += Vector3.up * jumpMultiplier;
        }
    }

    private void SetPlayerGrounded(bool _set)
    {
        isPlayerGrounded = _set;

        if (_set)
        {
            playerRb.drag = 5;
            playerRb.useGravity = false;
        }
        else
        {
            playerRb.drag = 0.001f;
            playerRb.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.collider.gameObject.tag;
        if("Ground" == tag || "Box" == tag)
            SetPlayerGrounded(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        string tag = collision.collider.gameObject.tag;
        if ("Ground" == tag || "Box" == tag)
            SetPlayerGrounded(false);
    }
}
