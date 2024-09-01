using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bass_MousePointer : MonoBehaviour
{
   
    private void Update()
    {

        //メインカメラ上のマウスカーソルのある位置からRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Rayが当たるオブジェクトがあった場合はそのオブジェクト名をログに表示
            Debug.Log(hit.collider.gameObject.name);
        }

    }

}
