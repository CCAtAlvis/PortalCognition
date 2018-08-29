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

    /// <summary>
    /// Add time (in sec) to display timer
    /// </summary>
    /// <param name="_time">time in seconds</param>
    public void AddTime(float _time)
    {
        displayTimer += _time;
    }

    /// <summary>
    /// stop the game!
    /// </summary>
    private void StopGame()
    {
        Time.timeScale = 0;
    }
}
