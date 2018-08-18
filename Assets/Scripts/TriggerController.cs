using UnityEngine;
using UnityEngine.Networking;

public class TriggerController : NetworkBehaviour
{
    public GameObject triggerDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (!isServer)
            return;

        //if ("Box" != other.tag || "Player" != other.tag)
        //    return;

        if ("Box" == other.tag)
        {
            Debug.Log("box entered the trigger.");
        }

        if ("Player" == other.tag)
        {
            Debug.Log("player entered the trigger.");
        }

        RpcSetTrigger(false);
        triggerDoor.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.LogError("something exited");
        if (!isServer)
            return;

        RpcSetTrigger(true);
        triggerDoor.SetActive(true);
    }

    [ClientRpc]
    private void RpcSetTrigger(bool _set)
    {
        //triggerDoor.SetActive(triggerDoor.activeSelf ^ true);
        triggerDoor.SetActive(_set);
    }
}
