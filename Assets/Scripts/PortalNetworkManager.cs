using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PortalNetworkManager : NetworkManager
{
	[System.Serializable]
	public class Display
	{
		public GameObject startGameButton;
		public GameObject stopGameButton;
		public GameObject restartGameButton;
		public GameObject canvas;
	}
	public Display display;

	[System.Serializable]
	public class Prefabs
	{
        public GameObject bullet;
		public GameObject blue;
		public GameObject red;
	}
    //public Prefabs prefabs;

    public GameObject portalBlue;
	public GameObject portalRed;
    public GameObject bulletBlue;
    public GameObject bulletRed;

    public struct NIDS
    {
        public NetworkInstanceId portalBlue;
        public NetworkInstanceId portalRed;
        public NetworkInstanceId bulletBlue;
        public NetworkInstanceId bulletRed;
    };
    private NIDS nids;

    public PortalGameManager PGM;
    public bool startAsServer;

    private static int noOfPlayer = 0;
    private NetworkConnection[] connections = new NetworkConnection[2];
    private GameObject[] players = new GameObject[2];

    //public GameObject player1;
    //public GameObject player2;

    private void Start()
	{
        nids.portalBlue = portalBlue.GetComponent<NetworkIdentity>().netId;
        nids.bulletBlue = bulletBlue.GetComponent<NetworkIdentity>().netId;

        nids.portalRed = portalRed.GetComponent<NetworkIdentity>().netId;
        nids.bulletRed = bulletRed.GetComponent<NetworkIdentity>().netId;

        if (startAsServer)
        {
            PortalStartGameServer();
			display.canvas.SetActive(true);
        }
    }

    public override void OnStartServer() { }

    public override void OnServerConnect(NetworkConnection conn)
    {
        //Debug.Log("Incoming player connection");
        connections[noOfPlayer] = conn;
        noOfPlayer++;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = Instantiate(playerPrefab, GetStartPosition());
        PlayerGunController pgc = player.GetComponent<PlayerGunController>();
        //players[noOfPlayer - 1] = player;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        NetworkServer.Spawn(player);

        if (1 == noOfPlayer)
		{
            PGM.InitPlayer(conn, player, portalBlue, portalRed, bulletBlue);
            //pgc.SetPlayer(1, portalBlue, portalRed, bulletBlue);
            //pgc.InitPlayer(nids.portalBlue, nids.portalRed, nids.bulletBlue);
        }
        else if (2 == noOfPlayer)
        {
            PGM.InitPlayer(conn, player, portalRed, portalBlue, bulletRed);
            //pgc.SetPlayer(1, portalRed, portalBlue, bulletRed);
            //pgc.InitPlayer(nids.portalRed, nids.portalBlue, nids.bulletRed);
        }
    }























    public override void OnStopServer()
    {
		if(!startAsServer)
        	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PortalStartGameServer()
    {
        //Debug.Log("Starting Server...");
        StartServer();
        Time.timeScale = 1;
		display.startGameButton.SetActive(false);
		display.stopGameButton.SetActive(true);
		display.restartGameButton.SetActive(true);
    }

    public void PortalRestartGameServer()
    {
        //Debug.Log("Initiating restart sequence...");
		PortalStopGameServer ();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PortalStopGameServer()
    {
        //Debug.Log("Stoping game server...");
        StopServer();
        PGM.StopGame();
	}

    public void PortalStartGameClient()
    {
        Debug.Log("Starting Client...");
        StartClient();
    }
}
