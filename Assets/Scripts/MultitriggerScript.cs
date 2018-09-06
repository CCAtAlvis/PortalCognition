using UnityEngine;

public class MultitriggerScript : MonoBehaviour
{
    public GameObject[] multiTriggers = new GameObject[4];
    public Material defaultMaterial;
    public Material newMaterial;
    //public int[] order = new int[4];
    private int i;

    void Start()
    {
        i = 0;
    }

    public void ChildTriggered(int index, GameObject g)
    {
        if (i < 4)
        {
            Debug.Log(index);
            if (i == index + 1)
                return;
            if (g == multiTriggers[i])
            {
                Debug.Log("Triggered");
                Debug.Log(i);
                g.GetComponent<MeshRenderer>().material = newMaterial;
                i++;
            }
            else
            {
                for (int j = i; j >= 0; j--)
                {
                    multiTriggers[j].GetComponent<MeshRenderer>().material = defaultMaterial;
                }
                i = 0;
            }
        }

        /* if(i == index)
         {
             Debug.Log(i);
             g.GetComponent<MeshRenderer>().material = newMaterial;
             i++;
         }
         else
         {
             for(int j=i;j>=0;j--)
             {
                 GameObject child = gameObject.transform.GetChild(j).gameObject;
                 child.GetComponent<MeshRenderer>().material = defaultMaterial;
             }
             i = 0;
         }*/
    }

    void Update()
    {
        if (i == 4)
        {
            Debug.Log("MultiTrigger Correctly completed");
            //this.enabled = false;
        }
    }
}
