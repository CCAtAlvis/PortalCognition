using UnityEngine;
using UnityEngine.Networking;

public class PortalNetworkDiscovery : NetworkDiscovery
{
    private bool startAsServer;
    private PortalNetworkManager pnm;
    private bool isConnected = false;

    private void Start()
    {
        pnm = GetComponent<PortalNetworkManager>();
        startAsServer = pnm.startAsServer;
        Initialize();

        if(startAsServer)
            StartAsServer();
        else
            StartAsClient();
    }

    public void StopServerBroadcast()
    {
        StopBroadcast();
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        Debug.LogError("broadcast received");
        pnm.networkAddress = fromAddress;
        pnm.PortalStartGameClient();
        Debug.Log("client connected");
        StopServerBroadcast();
    }
}
