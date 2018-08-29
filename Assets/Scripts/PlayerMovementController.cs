using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovementController : NetworkBehaviour
{
    [System.Serializable]
    public class Multiplier
    {
        public float speed = 3;
        public float jump = 4;
    }

    [System.Serializable]
    public class Player
    {
        public GameObject mesh;
        public Rigidbody rigidbody;
        public Camera camera;
    }

    public Multiplier mul;
    public Player player;

    private bool isPlayerGrounded = false;

    public struct Init
    {
        public Vector3 position;
        public Transform parent;
    };
    private Init init;

    private void Start()
    {
        init.position = transform.position;
        init.parent = transform.parent;
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        Quaternion camRotation = player.camera.transform.rotation;
        camRotation.x = 0;
        camRotation.z = 0;

        player.mesh.transform.rotation = camRotation;
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        Vector3 camForward = player.camera.transform.forward;
        camForward.y = 0;

        //Handle ~all~ movement game input after this
        if (!isPlayerGrounded)
            return;

        float temp = player.rigidbody.velocity.y;
        Vector3 velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            //playerRb.AddForce(camForward.normalized * forceMultiplier);
            //playerRb.AddForce(playerMesh.transform.forward * forceMultiplier);
            velocity = player.mesh.transform.forward * mul.speed;
        }

        if (Input.GetKey(KeyCode.S))
            velocity = -player.mesh.transform.forward * mul.speed;

        player.rigidbody.velocity = new Vector3(velocity.x, temp, velocity.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.rigidbody.velocity += Vector3.up * mul.jump;
            transform.parent = null;
            Debug.Log("jumping with parent: " + transform.parent);
        }
    }

    private void SetPlayerGrounded(bool _set)
    {
        isPlayerGrounded = _set;

        if (_set)
        {
            player.rigidbody.drag = 5;
            player.rigidbody.useGravity = false;
        }
        else
        {
            player.rigidbody.drag = 0.001f;
            player.rigidbody.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.collider.gameObject.tag;
        if ("Ground" == tag || "Box" == tag)
            SetPlayerGrounded(true);
    }

    private void OnCollisionStay(Collision collision)
    {
        string tag = collision.collider.gameObject.tag;
        if ("Ground" == tag || "Box" == tag)
            SetPlayerGrounded(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        string tag = collision.collider.gameObject.tag;
        if ("Ground" == tag || "Box" == tag)
            SetPlayerGrounded(false);
    }

    public void Destroy()
    {
        transform.position = init.position;
        transform.parent = init.parent;
    }
}
