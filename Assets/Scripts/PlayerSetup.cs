using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    public Behaviour[] thingsToDisable;
    private Camera cam;
    private NetworkConnection conn;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < thingsToDisable.Length; i++)
                thingsToDisable[i].enabled = false;
        }
        else
        {
            Camera.main.gameObject.SetActive(false);
            ChangeRendererRecursively(transform, false);
        }
    }

    public void SetPlayerConnection(NetworkConnection _conn)
    {
        conn = _conn;
    }

    private void ChangeRendererRecursively(Transform _trans, bool _set)
    {
        Renderer ren = _trans.gameObject.GetComponent<Renderer>();
        if (null != ren)
            ren.enabled = _set;

        foreach (Transform child in _trans)
        {
            ChangeRendererRecursively(child, _set);
        }
    }
}
