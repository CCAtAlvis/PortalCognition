using UnityEngine;

public class LaserBeamEnemy : MonoBehaviour
{
	public float toggleTime;
	public GameObject beams;

	private float timer;

	void Update ()
	{
		timer += Time.deltaTime;

		if (timer > toggleTime)
		{
			beams.SetActive (beams.activeSelf ^ true);
			timer = 0;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if ("Box" == other.tag || "Player" == other.tag)
		{
			//TODO: make a script Destroy or ResetObject
			//so all Destroy functions can be accessed easily..
		}
	}
}
