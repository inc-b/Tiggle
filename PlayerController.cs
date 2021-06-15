using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movSpeed = 1f;
    public bool isActive;
    public bool isJoined;
    public GameObject playerModel;
    public GameObject joinedModel;
    public GameObject unjoinedModel;
    public Rigidbody rb;
    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isJoined = true;
        isActive = true;
        isPaused = false;
        SetJoinedModel();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused) {
            rb.isKinematic = true;
        } else {
            rb.isKinematic = false;
            // Move the active player around the world (or both characters if they're joined)
            if (isActive || isJoined) {
                float xDir = Input.GetAxis("Horizontal");
                float yDir = Input.GetAxis("Vertical");

                Vector3 force = new Vector3(xDir, 0, yDir) * movSpeed;
                rb.AddForce(force);
                if (force.magnitude > 0) {
                    joinedModel.GetComponent<AnimationControlScript>().StartRun();
                    unjoinedModel.GetComponent<AnimationControlScript>().StartRun();
                    playerModel.transform.localRotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
                } else {
                    joinedModel.GetComponent<AnimationControlScript>().StopRun();
                    unjoinedModel.GetComponent<AnimationControlScript>().StopRun();
                }
            } else {
                joinedModel.GetComponent<AnimationControlScript>().StopRun();
                unjoinedModel.GetComponent<AnimationControlScript>().StopRun();
            }
        }
    }

    // Switch out the displayed model to the joined one
    public void SetJoinedModel() {
        joinedModel.SetActive(true);
        unjoinedModel.SetActive(false);
    }

    // Switch out the displayed model to the unjoined one
    public void SetUnjoinedModel() {
        joinedModel.SetActive(false);
        unjoinedModel.SetActive(true);
    }

    public void Pause() {
        isPaused = true;
    }

    public void UnPause() {
        isPaused = false;
    }
}
