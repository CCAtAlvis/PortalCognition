using UnityEngine;

public class MultiTriggerPoint : MonoBehaviour
{
    public MultiTrigger trigger;
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        //if (!isServer)
        //    return;

        //Debug.Log(other.tag);

        if ("Box" != other.tag && "Player" != other.tag)
            return;

        //if ("Box" == other.tag)
        //{
        //    Debug.Log("box entered the trigger.");
        //}

        //if ("Player" == other.tag)
        //{
        //    Debug.Log("player entered the trigger.");
        //}

        Debug.Log(isTriggered);
        if (!isTriggered)
        {
            trigger.IncreaseTriggerCount();
            Debug.Log("++ trigger count");
        }

        isTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //if (!isServer)
        //    return;

        Debug.LogError("something exited");

        trigger.DeceaseTriggerCount();
        isTriggered = false;
    }
}
