using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCounter : MonoBehaviour
{
    public int switchcount;
    private Vector3 Npos;
    private Vector3 upPos;
    private Vector3 downPos;
    private float speed = 3.0f;
    private bool okNot;

    // Start is called before the first frame update
    void Start()
    {
        switchcount = 0;
        upPos = new Vector3(0.0f, 3.0f, 0.0f);
        downPos = new Vector3(0.0f, -3.0f, 0.0f);
        okNot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (switchcount == 5 && okNot == false)
        {
            Npos = this.gameObject.transform.position;
            this.gameObject.transform.position = Vector3.MoveTowards(Npos, Npos + upPos, speed);
            okNot = true;
        }

        if (switchcount < 5 && okNot == true)
        {
            Npos = this.gameObject.transform.position;
            this.gameObject.transform.position = Vector3.MoveTowards(Npos, Npos + downPos, speed);
            okNot = false;
        }
    }
}
