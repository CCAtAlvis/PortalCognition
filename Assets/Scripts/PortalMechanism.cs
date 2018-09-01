using UnityEngine;

public class PortalMechanism : MonoBehaviour
{
    public GameObject incomingPlayer;
    public GameObject otherPortal;
    private GameObject player;
    private Rigidbody playerrb;

    // Use this for initialization
    void Start()
    {
        incomingPlayer = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Triggered");
            player = other.gameObject;
            if (incomingPlayer == player)
            {
                Debug.Log("same incoming player");
                return;
            }
            if (otherPortal.activeSelf == false)
                return;
            PortalMechanism pm = otherPortal.GetComponent<PortalMechanism>();
            pm.incomingPlayer = player;
            playerrb = player.GetComponent<Rigidbody>();
            player.SetActive(false);
            Vector3 vel = playerrb.velocity;
            playerrb.isKinematic = true;
            float velMagnitute = vel.magnitude;
            Debug.Log(velMagnitute);
            player.transform.position = otherPortal.transform.position;
            Debug.Log(player.transform.position);
            //player.transform.forward = otherPortal.transform.forward;
            playerrb.isKinematic = false;
            playerrb.velocity = otherPortal.transform.forward * velMagnitute;
            Debug.Log(playerrb.velocity.magnitude);
            player.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        incomingPlayer = null;
        this.gameObject.SetActive(false);
        otherPortal.SetActive(false);
    }
}
