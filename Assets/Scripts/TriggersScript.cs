using UnityEngine;

public class TriggersScript : MonoBehaviour
{
    public int index;

    void OnTriggerEnter(Collider other)
    {
        if ("Player" == other.tag)
            gameObject.GetComponentInParent<MultitriggerScript>().ChildTriggered(index, gameObject);
    }
}
