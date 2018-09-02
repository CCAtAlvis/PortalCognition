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
	public Prefabs prefabs;
	private GameObject portalBlue;
	private GameObject portalRed;
    private GameObject bulletBlue;
    private GameObject bulletRed;

    public PortalGameManager PGM;
    public bool startAsServer;

    private static int noOfPlayer = 0;
    private NetworkConnection[] connections = new NetworkConnection[2];
    private GameObject[] players = new GameObject[2];

    public GameObject player1;
    public GameObject player2;

    private void Start()
	{
        if (startAsServer)
        {
            PortalStartGameServer();
			display.canvas.SetActive(true);
        }
    }

    public override void OnStartServer()
    {
        //Debug.Log("Server Started.");
        //portalBlue = Instantiate(prefabs.blue);
        ////NetworkServer.Spawn(portalBlue);
        //portalBlue.SetActive(false);
        //bulletBlue = Instantiate(prefabs.bullet);
        ////NetworkServer.Spawn(bulletBlue);
        //bulletBlue.SetActive(false);

        //portalRed = Instantiate(prefabs.red);
        ////NetworkServer.Spawn(portalRed);
        //portalRed.SetActive(false);
        //bulletRed = Instantiate(prefabs.bullet);
        ////NetworkServer.Spawn(bulletRed);
        //bulletRed.SetActive(false);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        //Debug.Log("Incoming player connection");
        connections[noOfPlayer] = conn;
        noOfPlayer++;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = Instantiate(playerPrefab, GetStartPosition());
        players[noOfPlayer - 1] = player;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        NetworkServer.Spawn(player);

        if (2 == noOfPlayer)
		{
            //NetworkServer.AddPlayerForConnection(conn, player2, playerControllerId);
            //PGM.();
            //pcg.SetPlayer (2, portalRed, portalBlue, bulletRed);
            PGM.InitPlayers(connections, players);
        }
        if (1 == noOfPlayer)
        {
            //NetworkServer.AddPlayerForConnection(conn, player1, playerControllerId);
            //player1.GetComponent<PlayerSetup> ().StartUp();
            //pcg.SetPlayer (1, portalBlue, portalRed, bulletBlue);
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
