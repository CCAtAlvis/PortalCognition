using UnityEngine;

public class PlatformDependantComplier : MonoBehaviour
{
    public GameObject display;
    public GameObject GVR;

    // Use this for initialization
    void Start()
    {
#if UNITY_STANDALONE
        //Debug.Log ("standalone");
        display.SetActive(true);
        //GVR.SetActive(false);
#endif

#if UNITY_ANDROID
			Debug.log("hello");
#endif
    }
}
