using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public WorldController worldController;
    GameObject target;
    GameObject newTarget;
    Vector3 targetPos;
    bool moving = false;
    float movSpeed = 1f;
    float distanceToTravel = 0;
    float camHeight = 15f;
    float camZOffset = -8f;

    // Start is called before the first frame update
    void Start()
    {
        target = worldController.human;
        targetPos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving) {
            Vector3 camPos = new Vector3(target.transform.position.x, camHeight, target.transform.position.z + camZOffset);
            transform.position = camPos;
        } else {
            targetPos = new Vector3(newTarget.transform.position.x, camHeight, newTarget.transform.position.z + camZOffset);
            Vector3 camPos = Vector3.Lerp(targetPos, transform.position, distanceToTravel);
            transform.position = camPos;
            distanceToTravel = distanceToTravel - (movSpeed * Time.deltaTime);

            if (distanceToTravel < .1f) {
                target = newTarget;
                distanceToTravel = 0f;
                moving = false;
            }
        }
    }

    public bool IsMoving() {
        return moving;
    }

    public void SetActiveCharacter(GameObject activeCharacter) {
        if (activeCharacter.name != target.name) {
            newTarget = activeCharacter;
            moving = true;
            distanceToTravel = 1f;
        }
    }
}
