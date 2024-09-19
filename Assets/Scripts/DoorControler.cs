using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class DoorControler : MonoBehaviour
{
    public float doorposition;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void Door()
    {
        GameObject door = GameObject.Find("Door");
        Vector3 pos = door.transform.position;
        Debug.Log("open");
        Vector3 target = new Vector3(pos.x, doorposition, pos.z);
        float step = 2.0f * Time.deltaTime;
        if (Vector3.Distance(pos, target) > 0.01f)
        {
            door.transform.position = Vector3.MoveTowards(pos, target, step);
        }
    }
}

