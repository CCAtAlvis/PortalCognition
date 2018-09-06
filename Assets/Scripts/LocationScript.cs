
using UnityEngine;

public class LocationScript : MonoBehaviour {

    public Transform destination;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        if("Player"==other.tag && destination != null)
        {
            Debug.Log("Playerhit");
            other.transform.position = destination.position;
            other.transform.rotation = destination.rotation;
        }
    }
}
