using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backnforth : MonoBehaviour
{
    float dirMod = 1f;
    float movSpeed = 5f;
    Vector3 position;
    float xLimit = 3f;

    // Start is called before the first frame update
    void Start()
    {
        position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = transform.position.x + ((movSpeed * Time.deltaTime) * dirMod);
        if (newX > xLimit || newX < -xLimit) {
            dirMod = -dirMod;
        }

        position = new Vector3(newX, 0, 0);
        transform.position = position;
    }
}
