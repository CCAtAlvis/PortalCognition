using UnityEngine;

public class LevelEnd : MonoBehaviour {

    public PortalGameManager pgm;
    void OnTriggerEnter(Collider other)
    {
        pgm.StopGame();
    }
}
