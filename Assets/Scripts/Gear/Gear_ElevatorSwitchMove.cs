using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear_ElevatorSwitchMove : MonoBehaviour
{
    public float elevatorposition;
    public GameObject clearArea;
    // Start is called before the first frame update
    void Start()
    {
        clearArea = GameObject.Find("clearArea");
        clearArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Bass_MousePointer mousepointer; //呼ぶスクリプトにあだなつける
        GameObject obj = GameObject.Find("Bass_MouseManager"); //Bass_MouseManagerっていうオブジェクトを探す
        mousepointer = obj.GetComponent<Bass_MousePointer>(); //付いているスクリプトを取得

        GameObject door = GameObject.Find("ElevatorSwitch");
        Vector3 pos = door.transform.position;
        Vector3 target = new Vector3(pos.x, elevatorposition, pos.z);


        if (mousepointer.GearClicked == 1)
        {
            clearArea.SetActive(true);



            float step = 2.0f * Time.deltaTime;
            if (Vector3.Distance(pos, target) > 0.01f)
            {
                door.transform.position = Vector3.MoveTowards(pos, target, step);
            }
        }
    }
}
