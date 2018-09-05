using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [System.Serializable]
    public class Settings
    {
		[HideInInspector]
        public Vector3 start;
        public Vector3 end;
        public float speed;
        public bool startOnTrigger;
        public bool teleportPlatform;
    }

    public Settings setting;
    private Vector3 direction;
    private bool movePlatform = false;

    // Use this for initialization
    void Start()
    {
        setting.start = transform.position;
        direction = setting.end - setting.start;
        direction = direction.normalized;

        if (!setting.startOnTrigger)
            movePlatform = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!movePlatform)
            return;

        if (Vector3.Distance(transform.position, setting.end) < 0.1f)
        {
            //teleport platform
            if (setting.teleportPlatform)
            {
                gameObject.SetActive(false);
                transform.position = setting.start;
                gameObject.SetActive(true);
            }
            else
            {
                Vector3 temp = setting.start;
                setting.start = setting.end;
                setting.end = temp;
                //this was dumb * faceplam *
                //direction = setting.start - setting.end;
                direction = setting.end - setting.start;
                direction = direction.normalized;
            }
        }
        else
        {
            transform.position += direction * Time.deltaTime * setting.speed;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Player" == other.tag || "Box" == other.tag)
        {
            other.transform.parent = transform;
            movePlatform = true;
        }
    }
}
