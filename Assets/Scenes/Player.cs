using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
            //movement += new Vector3(-1, 0, 0); ¶ (X²•ûŒü)
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
            //movement += new Vector3(1, 0, 0);  ‰E (X²•ûŒü)
            //movement += new Vector3(1, 0, 0);  ‰E (X²•ûŒü)
        }

        if (Input.GetKey(KeyCode.Space))
        {
            movement += Vector3.up;
        }

        rb.AddForce(movement * speed);
    }
}

