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
	private GameObject portalBulletBlue;
	private GameObject portalBulletRed;

//	public void InitPlayers(NetworkConnection[] connections, GameObject[] players)
//	{
//		portalBlue = Instantiate(prefabs.blue);
//		portalBlue.SetActive(false);
//		portalRed = Instantiate(prefabs.red);
//		portalRed.SetActive(false);
//		portalBulletBlue = Instantiate(prefabs.bullet);
//		portalBulletBlue.SetActive(false);
//		portalBulletRed = Instantiate(prefabs.bullet);
//		portalBulletRed.SetActive(false);
//
//		TargetInit(connections[0], players[0], portalBlue, portalRed, portalBulletBlue);
//		//TargetInit(connections[1], portalRed, portalBlue, portalBulletRed);
//	}
//
//	[TargetRpc]
//	private void TargetInit (NetworkConnection target, GameObject player, GameObject _self, GameObject _other, GameObject _bullet)
//	{
//		PlayerGunController pgc = player.GetComponent<PlayerGunController> ();
//		pgc.self = _self;
//		pgc.other = _other;
//		pgc._bullet = _bullet;
//		pgc.Init ();
//	}


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
