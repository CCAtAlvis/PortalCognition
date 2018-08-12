using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
    public Behaviour[] thingsToDisable;
    public GameObject ignoreP1Cam, ignoreP2Cam;
    private Camera cam;
    //public Transform camHolder;

    private NetworkConnection conn;

    private void Start()
    {
        //Debug.Log(conn);
        if (!isLocalPlayer)
        {
            for (int i = 0; i < thingsToDisable.Length; i++)
            {
                thingsToDisable[i].enabled = false;
                //Debug.Log(thingsToDisable[i] + " : " + thingsToDisable[i].isActiveAndEnabled);
            }
        }
        else
        {
            Camera.main.gameObject.SetActive(false);
        }
    }

    public void SetPlayerConnection(NetworkConnection _conn)
    {
        conn = _conn;
        if(conn.connectionId == 1)
        {
            //cam = ignoreP1Cam;
            ignoreP2Cam.SetActive(false);

        }
        else
        {
            //cam = ignoreP2Cam;
            ignoreP1Cam.SetActive(false);
        }

        //Debug.Log(conn);
        gameObject.layer = (10 + conn.connectionId);
        ChangeLayersRecursively(this.gameObject.transform, gameObject.layer);
        //cam.cullingMask &= ~(1 << (10 + conn.connectionId));
        //Debug.Log(cam.cullingMask);

        //Camera.main.transform.parent = camHolder.transform;
        //Camera.main.cullingMask &= ~(1 << (10 + conn.connectionId));
    }

    public static void ChangeLayersRecursively(Transform _trans, int _layer)
    {
        _trans.gameObject.layer = _layer;
        foreach (Transform child in _trans)
        {
            ChangeLayersRecursively(child, _layer);
        }
    }
}
