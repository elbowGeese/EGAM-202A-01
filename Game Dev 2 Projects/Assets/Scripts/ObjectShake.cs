using System.Collections;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    public IEnumerator ShakeObject(float strength, float duration)
    {
        while(duration > 0)
        {
            transform.position = startPos + new Vector3(Random.Range(-strength, strength), Random.Range(-strength, strength), 0f);
            duration -= Time.deltaTime;

            yield return null;
        }

        transform.position = startPos;
    }
}
