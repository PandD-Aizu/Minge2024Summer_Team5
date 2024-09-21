using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControler : MonoBehaviour
{
    private Vector3 pos;
    private Vector3 start;
    public float FloorPosition = 3;
    public float movespeed = 0.01f;

    public void Start()
    {
        start = transform.position;
    }
    public void MoveUp()
    {
        StartCoroutine("FloorMove");
    }

    public void MoveDown()
    {
        StartCoroutine("FloorDown");
    }

    IEnumerator FloorMove()
    {
        while (pos.y < FloorPosition)
        {
            pos = transform.position;
            transform.Translate(0, movespeed, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator FloorDown()
    {
        while (pos.y > start.y)
        {
            pos = transform.position;
            transform.Translate(0, -movespeed, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
