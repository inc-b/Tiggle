using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControlScript : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isRunning", false);
    }

    // Update is called once per frame
    void Update()
    {  
    }

    public void StartRun() {
        animator.SetBool("isRunning", true);
    }

    public void StopRun() {
        animator.SetBool("isRunning", false);
    }
}
