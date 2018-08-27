using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PortalNetworkManager : NetworkManager
{
    private static int noOfPlayer = 0;
    private NetworkConnection[] connections = new NetworkConnection[2];

    public GameObject startGameButton;
    public GameObject stopGameButton;
    public GameObject restartGameButton;

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

    public void PortalStartGameServer()
    {
        //Debug.Log("Starting Server...");
        StartServer();
        startGameButton.SetActive(false);
        stopGameButton.SetActive(true);
        restartGameButton.SetActive(true);
    }

    public void PortalStartGameClient()
    {
        //Debug.Log("Starting Client...");
        StartClient();
    }

    public void PortalRestartGameServer()
    {
        //Debug.Log("Initiating restart sequence...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PortalStopGameServer()
    {
        //Debug.Log("Stoping game server...");
        startGameButton.SetActive(true);
        stopGameButton.SetActive(false);
        restartGameButton.SetActive(false);

    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        //Debug.Log("Player added to server. Player id: " + playerControllerId);
        //Debug.Log("player conn: " + conn);

        // base.OnServerAddPlayer(conn, playerControllerId);
        GameObject player = Instantiate(playerPrefab, GetStartPosition());
        //player.GetComponent<PlayerSetup>().SetPlayerConnection(conn);
		player.GetComponent<PlayerController> ().SetPlayerID (noOfPlayer);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        NetworkServer.Spawn(player);
    }
}
