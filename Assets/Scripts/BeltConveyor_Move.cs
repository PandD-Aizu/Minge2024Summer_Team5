using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyor_Move : MonoBehaviour
{
    private Vector3 positionOffset = new Vector3(5.0f, 0, 0);
    public float speed_onbelt = 5.0f;
    public int ConveyorSwitch = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(ConveyorSwitch == 0)
        {
            positionOffset = new Vector3(speed_onbelt, 0, 0);  // 固定値を設定

        }else if(ConveyorSwitch == 1)
        {
            positionOffset = new Vector3(speed_onbelt * (-1), 0, 0);  // 固定値を設定
        }

    }

    void OnTriggerStay(Collider other)/*オブジェクトが重なっている間*/
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("attached");
            // 重なっているオブジェクトのTransformを取得
            Transform playerTransform = other.transform;

            // プレイヤーの位置にオフセットを加算し続ける
            playerTransform.position += positionOffset * Time.deltaTime;  // Time.deltaTimeでフレームに依存しない加算を実現
        }
        
    }

    

}
