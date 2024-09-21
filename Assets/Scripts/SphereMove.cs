using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMove : MonoBehaviour
{
    public GameObject ElevatorFloor;

    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        float dz = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.position = new Vector3(
            transform.position.x + dx, transform.position.y, transform.position.z + dz
            );
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

