using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor_Door_Switch : MonoBehaviour
{
    public float switchposition;
    public bool SwitchON = false;
    public BeltConveyor_Move conveyorswitch; //呼ぶスクリプトにあだなつける
    private GameObject gearObject;
    private GameObject yazirushi;
    private GameObject lever;
    public float GearSpeed = 50.0f;
    public bool rotate180 = false;
    public bool rotate90 = false;
    public bool OnceSwitch = true;
    public bool SwitchMovable = false;

    // Start is called before the first frame update
    void Start()
    {
        gearObject = GameObject.FindWithTag("ConveyorGear");
        yazirushi = GameObject.Find("yazirushi");
        lever = GameObject.Find("lever");
    }

    // Update is called once per frame
    void Update()
    {
        /*Switchを動かす用*/
        GameObject door_s = GameObject.Find("Switch");
        Vector3 pos = door_s.transform.position;
        Vector3 target = new Vector3(pos.x, switchposition, pos.z);
        float step = 2.0f * Time.deltaTime;

        /*BeltConveyor_MoveからConveyorSwitchを引っ張る用*/
        GameObject obj = GameObject.Find("ConveyorAttach"); //ConveyorAttachっていうオブジェクトを探す
        conveyorswitch = obj.GetComponent<BeltConveyor_Move>(); //付いているスクリプトを取得

        Vector3 rotationSpeed = new Vector3(0, 0, GearSpeed);  // コンベヤのギア回転位置

        if(SwitchON == false)
        {
            //gearObject.transform.Rotate(rotationSpeed * Time.deltaTime);// コンベヤのギア回転
        }

        if (SwitchMovable)
        {
            if (Vector3.Distance(pos, target) > 0.01f)/*Switchが動く*/
            {
                door_s.transform.position = Vector3.MoveTowards(pos, target, step);
                Debug.Log("SwitchMove");
            }
        }

        if (SwitchON)
        {
            SwitchMovable = true;
            

            //gearObject.transform.Rotate((rotationSpeed * (-1)) * Time.deltaTime);// コンベヤのギア逆回転

            conveyorswitch.ConveyorSwitch = 1;/*ConveyorSwitchをonにする*/

            rotate180 = true;

            rotate90 = true;
            SwitchON = false;
        }

        if (rotate180)
        {
            yazirushi.transform.Rotate(0, 0, 180);
            rotate180 = false;
        }

        if (rotate90)
        {
            lever.transform.Rotate(0, 0, -90);
            rotate90 = false;
        }
    }

    void OnTriggerEnter(Collider other)/*プレイヤーが重なった時*/
    {
        if (OnceSwitch && other.CompareTag("Player"))
        {
            Debug.Log("attached_Switch");

            SwitchON = true;
            OnceSwitch = false;
        }

    }
    
}
