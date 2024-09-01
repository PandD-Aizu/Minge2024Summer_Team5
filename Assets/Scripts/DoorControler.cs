using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour
{
    public Animator animator;
    public bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isOpen)
            {
                animator.SetBool("open", true);
                isOpen = true;
            }
            else
            {
                animator.SetBool("open", false);
                isOpen = false;
            }
        }
        

    }
}