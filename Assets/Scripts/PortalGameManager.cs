using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PortalGameManager : NetworkBehaviour
{
    [Header("Display info")]
    public Text timerUI;
    public Text checkpointsUI;
    public GameObject display;

    [Header("Timers")]
    [SerializeField]
    private float actualTimer; /*time to play*/
    [SerializeField]
    private float displayTimer; /*Time shown to event heads*/
    private int timer;

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

    public void InitPlayer(NetworkConnection _conn, GameObject _player, int _index)
    {
        TargetInit(_conn, _player, _index);
    }

    [TargetRpc]
    private void TargetInit(NetworkConnection target, GameObject player, int _index)
    {
        PlayerGunController pgc = player.GetComponent<PlayerGunController>();
        pgc.InitPlayer(_index);
    }

    private void Update()
    {
        if (!isServer)
            return;

        actualTimer -= Time.deltaTime;
        displayTimer += Time.deltaTime;
        timer = (int)displayTimer;
        timerUI.text = "Time:" + timer.ToString();
    }

    public void StartGame()
    {
        timerUI.text = "Time: ";
        checkpointsUI.text = "Checkpoints: ";
    }

    public void AddTime(float _time)
    {
        displayTimer += _time;
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }
}
