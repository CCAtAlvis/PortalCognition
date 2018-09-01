using UnityEngine;
using UnityEngine.Networking;

public class PortalNetworkDiscovery : NetworkDiscovery
{
	private PortalNetworkManager pnm;
	private bool isConnected = false;

	private void Start()
	{
		pnm = GetComponent<PortalNetworkManager> ();
		Initialize ();

		#if UNITY_ANDROID
		Debug.Log ("running on android");
		StartAsClient();
		#endif
	}

	public void StartServerBroadcast()
	{
		#if UNITY_STANDALONE
		StartAsServer();
		#endif	
	}

	public void StopServerBroadcast()
	{
		#if UNITY_STANDALONE
		StopBroadcast ();
		#endif
	}

	public override void OnReceivedBroadcast(string fromAddress, string data)
	{
		Debug.Log ("broadcast received");
		if (isConnected)
			return;

		pnm.networkAddress = fromAddress;
		pnm.PortalStartGameClient ();
		Debug.Log ("client connected");
		isConnected = true;
	}
}
