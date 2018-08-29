using UnityEngine;

public class LaserBeamEnemy : MonoBehaviour
{
    public float toggleTime;
    public GameObject beams;
    public BoxCollider trigger;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > toggleTime)
        {
            beams.SetActive(beams.activeSelf ^ true);
            trigger.enabled = trigger.enabled ^ true;
            timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (/*"Box" == other.tag ||*/ "Player" == other.tag)
        {
            //TODO: make a script Destroy or ResetObject
            //so all Destroy functions can be accessed easily..
            other.GetComponent<PlayerMovementController>().Destroy();
        }
    }
}
