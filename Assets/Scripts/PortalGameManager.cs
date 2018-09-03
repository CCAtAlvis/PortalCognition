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

	public void InitPlayer(NetworkConnection _conn, GameObject _player, GameObject _self, GameObject _other, GameObject _bullet, int _index)
    {
//        Debug.LogError("server: " + _self);
//        Debug.LogError("server: " + _other);
//        Debug.LogError("server: " + _bullet);
//        NetworkInstanceId _s = _self.GetComponent<NetworkIdentity>().netId;
//        NetworkInstanceId _o = _other.GetComponent<NetworkIdentity>().netId;
//        NetworkInstanceId _b = _bullet.GetComponent<NetworkIdentity>().netId;
//        Debug.LogError(_s +"   :   "+ _o + "   :   " + _b);
//		TargetInit(_conn, _player, _s, _o, _b, _index);
		TargetInit(_conn, _player, _index);
    }

	[TargetRpc]
	private void TargetInit (NetworkConnection target, GameObject player, int _index) {
        PlayerGunController pgc = player.GetComponent<PlayerGunController>();
		pgc.InitPlayer(_index);
	}

//    [TargetRpc]
//	private void TargetInit(NetworkConnection target, GameObject player, NetworkInstanceId _self, NetworkInstanceId _other, NetworkInstanceId _bullet, int _index)
//    {
//        PlayerGunController pgc = player.GetComponent<PlayerGunController>();
//        Debug.LogError("TargetInit: " + _self + "   :   " + _other + "   :   " + _bullet);
//		pgc.InitPlayer(_self, _other, _bullet, _index);
//        //pgc.InitPlayer(_self, _other, _bullet);
//        //pgc.self = _self;
//        //pgc.other = _other;
//        //pgc._bullet = _bullet;
//        //pgc.Init();
//    }

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
