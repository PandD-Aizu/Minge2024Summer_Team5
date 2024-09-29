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

        if (area == null)
        {
            Debug.LogWarning("areaÇ™å©Ç¬Ç©ÇËÇ‹ÇπÇÒÅB");
        }
        else
        {
            area.GetComponent<Collider>().enabled = false;
        }
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

