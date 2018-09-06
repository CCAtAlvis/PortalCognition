
using UnityEngine;

public class Level2TriggerScript : MonoBehaviour {

    public GameObject StartPortal;
    public Transform loc1SpawnPoint;
    public Transform loc2SpawnPoint;
    public Transform loc3SpawnPoint;
    public Material loc1Color;
    public Material loc2Color;
    public Material loc3Color;
    private Material originalColor;
    private Material triggerColor;
    private Material StartPortalColor;
    LocationScript ls;
	// Use this for initialization
	void Start () {
        ls = StartPortal.GetComponent<LocationScript>();
        triggerColor = GetComponent<Renderer>().material;
        StartPortalColor = StartPortal.GetComponent<Renderer>().material;
        originalColor = triggerColor;
	}

    void OnTriggerEnter(Collider other)
    {
        if("LocationTrigger" == other.gameObject.tag)
        {
            Debug.Log("LocationTrigger");
            LocationType lt = other.GetComponent<LocationType>();
            if(lt.type==1)
            {
                triggerColor = loc1Color;
                StartPortalColor = loc1Color;
                Debug.Log("setToLocation1");
                ls.destination = loc1SpawnPoint;

            }
            if(lt.type==2)
            {
                triggerColor = loc2Color;
                StartPortalColor = loc2Color;
                Debug.Log("location2");
                ls.destination = loc2SpawnPoint;
            }
            if(lt.type==3)
            {
                triggerColor = loc3Color;
                StartPortalColor = loc3Color;
                Debug.Log("location 3");
                ls.destination = loc3SpawnPoint;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if ("LocationTrigger" == other.tag)
        {
            triggerColor = originalColor;
            StartPortalColor = originalColor;
            ls.destination = null;
        }
    }
}
