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
        public bool stopAtDest;
    }

    public Settings setting;
    private Vector3 direction;
    private bool movePlatform = false;

    void Start()
    {
        setting.start = transform.position;
        direction = setting.end - setting.start;
        direction = direction.normalized;

        if (!setting.startOnTrigger)
            movePlatform = true;
    }

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

            if (setting.stopAtDest)
                Destroy(this);
        }
        else
        {
            transform.position += direction * Time.deltaTime * setting.speed;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if ("Player" == collision.collider.tag || "Box" == collision.collider.tag)
        {
            collision.collider.transform.parent = transform;
            movePlatform = true;
        }
    }
}
