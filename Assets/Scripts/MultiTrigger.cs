using UnityEngine;

public class MultiTrigger : MonoBehaviour
{
    public int noOfTriggers = 0;
    public GameObject triggerObject;
    public bool alwaysOpen = true;

    private int activatedTriggers = 0;
    private bool firstTime = true;

    public void IncreaseTriggerCount()
    {
        //if (!isServer)
        //    return;

        activatedTriggers++;
        CheckTriggerCount();
    }

    public void DeceaseTriggerCount()
    {
        //if (!isServer)
        //    return;

        activatedTriggers--;
        CheckTriggerCount();
    }

    private void CheckTriggerCount()
    {
        Debug.Log(activatedTriggers + " : " + noOfTriggers);
        if (activatedTriggers == noOfTriggers && firstTime)
        {
            Debug.Log("in here");
            //RpcSetTrigger(false);
            triggerObject.SetActive(false);
            firstTime = false;
        }
        else if (activatedTriggers < noOfTriggers)
        {
            if (!alwaysOpen)
            {
                //RpcSetTrigger(true);
                triggerObject.SetActive(true);
            }
        }
    }

    //[ClientRpc]
    //private void RpcSetTrigger(bool _set)
    //{
    //    //triggerObject.SetActive(triggerObject.activeSelf ^ true);
    //    triggerObject.SetActive(_set);
    //}
}
