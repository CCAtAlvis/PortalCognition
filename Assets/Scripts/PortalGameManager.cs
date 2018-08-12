using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PortalGameManager : NetworkBehaviour
{
    public Text timeTextUI;
    public Text checkpointsTextUI;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        timeTextUI.text = "Time: ";
        checkpointsTextUI.text = "Checkpoints: ";
    }
}
