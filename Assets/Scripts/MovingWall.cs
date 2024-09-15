using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{

    private float speed = 20.0f;
    private int i;
    private Vector3 startPos;
    private Vector3 Npos;
    private Rigidbody Rb;
    private bool isRising = false;

    // Start is called before the first frame update
    void Start()
    {

        startPos = transform.position;
        Rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isRising)
        {
            WallUp();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //地面に当たったら上昇
        if (other.gameObject.CompareTag("Ground"))
        {
            Npos = transform.position;
            Invoke("switchRisingState", 1.0f);
        }
    }

    //元の位置まで上昇
    private void WallUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * speed);
        if(transform.position == startPos)
        {
            isRising = false;
        }

        //transform.position = Vector3.Lerp(Npos, startPos, Time.deltaTime*speed);

        //Debug.Log("move");
        /*for(i=0; i <= 5; i++)
        {
            transform.Translate(0.0f, 1.0f, 0.0f);
            
        }*/

    }

    private void switchRisingState()
    {
        if (!isRising)
        {
            isRising = true;
        }
        
    }
}
