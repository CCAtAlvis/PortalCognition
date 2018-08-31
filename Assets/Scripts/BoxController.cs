using UnityEngine;
using UnityEngine.Networking;

public class BoxController : NetworkBehaviour
{
    public struct Box
    {
        Vector3 initialPosition;
    };

    public void Destroy()
    {
    }
}
