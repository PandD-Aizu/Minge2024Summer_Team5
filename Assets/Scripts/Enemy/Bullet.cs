using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 4.0f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Dragable"))
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Ground")) 
        { 
            Destroy(this.gameObject);
        }
            
    }
}