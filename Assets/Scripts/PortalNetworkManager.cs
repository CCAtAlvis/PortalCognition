using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PortalNetworkManager : NetworkManager
{
    public GameObject startGameButton;
    public GameObject stopGameButton;
    public GameObject restartGameButton;
    public PortalGameManager PGM;

    private string ipAddress = "192.168.1.104";
    private int port = 7777;

    private static int noOfPlayer = 0;
    private NetworkConnection[] connections = new NetworkConnection[2];

    public override void OnStartServer()
    {
        //Debug.Log("Server Started.");
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        //Debug.Log("Incoming player connection");
        connections[noOfPlayer] = conn;
        noOfPlayer++;
        //Debug.Log("Player connected: no of players: " + noOfPlayer);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        //Debug.Log("Player added to server. Player id: " + playerControllerId);
        //Debug.Log("player conn: " + conn);

        // base.OnServerAddPlayer(conn, playerControllerId);
        GameObject player = Instantiate(playerPrefab, GetStartPosition());
        //player.GetComponent<PlayerSetup>().SetPlayerConnection(conn);
        player.GetComponent<PlayerGunController>().SetPlayerID(noOfPlayer);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        NetworkServer.Spawn(player);
    }

    public override void OnStopServer()
    {
#if UNITY_ANDROID
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
#endif
    }

    public void PortalStartGameServer()
    {
        //Debug.Log("Starting Server...");
        StartServer();
        Time.timeScale = 1;
        startGameButton.SetActive(false);
        stopGameButton.SetActive(true);
        restartGameButton.SetActive(true);
    }

    public void PortalRestartGameServer()
    {
        //Debug.Log("Initiating restart sequence...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PortalStopGameServer()
    {
        //Debug.Log("Stoping game server...");
        StopServer();
        PGM.StopGame();
        //startGameButton.SetActive(true);
        //stopGameButton.SetActive(false);
        //restartGameButton.SetActive(false);
    }

    public void PortalStartGameClient()
    {
        Debug.Log("Starting Client...");
        //networkAddress = ipAddress;
        //networkPort = port;
        StartClient();
    }
}
