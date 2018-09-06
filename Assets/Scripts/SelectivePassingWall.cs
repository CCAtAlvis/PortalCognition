using UnityEngine;

public class SelectivePassingWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ("Player" == other.tag)
        {
            //Destroy destroy = other.GetComponent<Destroy>();
            //if  (destroy != null)
            //{
            //    destroy.Destroy();
            //}
        }
    }
}
