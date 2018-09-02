using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    public Behaviour[] thingsToDisable;
    private Camera cam;
    public Transform sp;

    private void Start()
    {
        StartUp();
    }
    
    public void StartUp()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < thingsToDisable.Length; i++)
                thingsToDisable[i].enabled = false;
        }
        else
        {
            Camera.main.gameObject.SetActive(false);
            //Camera.main.transform.position = sp.position;
            //Camera.main.transform.rotation = sp.rotation;
            //Camera.main.transform.parent = sp;
            ChangeRendererRecursively(transform, false);
        }

        //Destroy(this);
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
