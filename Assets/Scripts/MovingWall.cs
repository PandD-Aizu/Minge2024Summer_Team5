using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{

    private float speed = 3;
    private Vector3 Pos;
    private Vector3 Npos;
    private Rigidbody Rb;

    // Start is called before the first frame update
    void Start()
    {

        Pos = transform.position;
        Rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        Npos = transform.position;

    }

    private void OnCollisionEnter(Collision collision)
    {
        //地面に当たったら上昇
        if (collision.gameObject.CompareTag("Ground"))
        {
            Invoke("WallUp", 1.0f);
        }

        //プレイヤーに当たったらプレイヤーを倒す
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject,1.0f);
        }
    }

    //元の位置まで上昇
    private void WallUp()
    {
        while(Npos.y >= Pos.y)
        {
            Rb.MovePosition(new Vector3(Npos.x, Npos.y + speed, Npos.z));
        }

    }
}
