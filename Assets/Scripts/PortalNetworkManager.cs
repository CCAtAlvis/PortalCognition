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
		public GameObject blue;
		public GameObject red;
	}
	public Prefabs prefabs;
	private GameObject portalBlue;
	private GameObject portalRed;

    public PortalGameManager PGM;
    public bool startAsServer;

    private static int noOfPlayer = 0;
    private NetworkConnection[] connections = new NetworkConnection[2];

    private void Start()
	{
		portalBlue = Instantiate(prefabs.blue);
		portalBlue.SetActive(false);
		portalRed = Instantiate(prefabs.red);
		portalRed.SetActive(false);

		if (startAsServer)
        {
            PortalStartGameServer();
			display.canvas.SetActive(true);
        }
    }

    public override void OnStartServer()
    {
        //Debug.Log("Server Started.");
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
		PlayerGunController pcg = player.GetComponent<PlayerGunController> ();

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        NetworkServer.Spawn(player);

		if (2 == noOfPlayer)
		{
			//PGM.();
			pcg.SetPlayer (2, portalRed, portalBlue);
		}
		if(1 == noOfPlayer)
		{
			pcg.SetPlayer (1, portalBlue, portalRed);
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
