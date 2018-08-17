using UnityEngine;

public class BadtiHuiJalebi : MonoBehaviour
{
    float timer;
    float completeTime = 10f;

    void Update()
    {
        float frac = timer / completeTime;
        Vector3 scaling = Vector3.Lerp(Vector3.zero, Vector3.one, frac);

        transform.localScale = scaling;

        timer += Time.deltaTime;
        if (timer > completeTime + 0.1f)
            Destroy(this);
    }
}
