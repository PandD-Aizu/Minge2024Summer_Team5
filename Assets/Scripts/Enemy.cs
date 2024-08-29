using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time, 1) * 2f + 1, transform.position.y, transform.position.z);
      //transform.position = new Vector3(Mathf.PingPong(Time.time, ˆÚ“®‹——£) * ˆÚ“®‘¬“x + ‰ŠúˆÊ’u, transform.position.y, transform.position.z);
    }
}
