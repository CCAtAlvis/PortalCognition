using UnityEngine;

public class PlatformDependantComplier : MonoBehaviour
{
	public GameObject display;
	// Use this for initialization
	void Start () {
		#if UNITY_STANDALONE
			Debug.Log ("standalone");
			display.SetActive(true);
		#endif

		#if UNITY_ANDROID
			Debug.log("hello");
		#endif
	}
}
