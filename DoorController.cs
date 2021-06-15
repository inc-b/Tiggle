using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Collider colliderMesh;

    public AudioClip doorOpen;
    public AudioClip doorClose;
    public AudioSource audioSource;
    public bool unlocked;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        colliderMesh = GetComponent<Collider>();
        if (unlocked) {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle() {
        if (unlocked) {
            audioSource.PlayOneShot(doorClose);
            unlocked = false;
            gameObject.SetActive(true);
        } else { 
            audioSource.PlayOneShot(doorOpen);
            unlocked = true;
            gameObject.SetActive(false);
        }
    }
}
