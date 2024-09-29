using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMove : MonoBehaviour
{
    public GameObject ElevatorFloor;
    GameObject area;

    public void Start()
    {
        area = GameObject.Find("Area");
        area.GetComponent<Collider>().enabled = false;
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Area")
        {
            ElevatorFloor.GetComponent<ElevatorControler>().MoveUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Area")
        {
            ElevatorFloor.GetComponent<ElevatorControler>().MoveDown();
        }
    }
 
}

