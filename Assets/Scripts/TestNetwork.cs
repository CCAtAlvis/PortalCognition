using UnityEngine;
using UnityEngine.Networking;

public class TestNetwork : NetworkBehaviour
{
    [SyncVar]
    private int trigger = 0;

    public GameObject go;

    // Update is called once per frame
    void Update()
    {
        if(isServer)
        {
            Debug.Log("trigger= " + trigger);

            if (trigger == 2)
                go.SetActive(false);
        }

        if (!isClient)
            return;
        Debug.Log("i am a client");

        if (Input.GetKey(KeyCode.Space))
        {
            Change();
            Debug.Log("hi there");
        }
    }

    void Change()
    {
        trigger++;
    }
}
