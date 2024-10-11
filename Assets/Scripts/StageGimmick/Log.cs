using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "LogDestroy")
        {
            Destroy(this.gameObject);
        }

    }
}
