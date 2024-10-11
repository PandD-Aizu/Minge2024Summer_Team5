using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchcounter : MonoBehaviour
{
    [SerializeField] GameObject player;
    private float timelapse;
    private float switchtime = 50.0f;
    private bool onoff;
    private Vector3 pos;
    private Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        timelapse = 0.0f;
        onoff = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onoff == true)
        {
            timelapse += Time.deltaTime;

            if (timelapse >= switchtime)
            {
                SwitchUp();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SwitchDown();
        }
    }

    private void SwitchUp()
    {
        myTransform = this.transform;
        pos = myTransform.position;
        pos.y += 0.3f;
        myTransform.position = pos;

        onoff = false;
        timelapse = 0.0f;
        SwitchCounter switchCounter;
        GameObject obj = GameObject.Find("SwitchCounter");
        switchCounter = obj.GetComponent<SwitchCounter>();
        switchCounter.switchcount -= 1;
    }

    private void SwitchDown()
    {
        myTransform = this.transform;
        pos = myTransform.position;
        pos.y += -0.3f;
        myTransform.position = pos;

        onoff = true;
        SwitchCounter switchCounter;
        GameObject obj = GameObject.Find("SwitchCounter");
        switchCounter = obj.GetComponent<SwitchCounter>();
        switchCounter.switchcount += 1;

    }

}