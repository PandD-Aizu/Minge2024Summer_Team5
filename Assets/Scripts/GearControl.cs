using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearControl : MonoBehaviour
{
    public float RotationSpeed = 1.0f;
    public int gearclicked_test = 0;/*test用*/


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // xyz軸を軸にして1度、回転させているQuaternionを作成
        Quaternion move_q = Quaternion.Euler(0f, 0f, RotationSpeed);
        // 自身のQuaternionを取得
        Quaternion q = this.transform.rotation;

        Bass_MousePointer mousepointer; //呼ぶスクリプトにあだなつける
        GameObject obj = GameObject.Find("Bass_MouseManager"); //Bass_MouseManagerっていうオブジェクトを探す
        mousepointer = obj.GetComponent<Bass_MousePointer>(); //付いているスクリプトを取得


        if (gearclicked_test == 1)
        {
            mousepointer.GearClicked = 1;
            
        } 



        if (mousepointer.GearClicked == 1)/*回転させる*/
        {
            Debug.Log("gear rotate");
            this.transform.rotation = q * move_q;
        }
    }
}
