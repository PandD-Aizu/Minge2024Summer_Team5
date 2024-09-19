using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongAttack : MonoBehaviour
{
    Collider bullet_collider;
    [SerializeField] private Renderer LongAttack;
    [SerializeField] GameObject target;
    [SerializeField] GameObject startEnemy;
    private Vector3 startPos;
    private float speed = 1.0f;
    private float shoottime = 1.0f;
    private float returntime = 3.0f;
    private float timelapse;

    // Start is called before the first frame update
    void Start()
    {
        LongAttack.enabled = false;
        bullet_collider = GetComponent<Collider>();
        bullet_collider.isTrigger = false;
    }
 
    // Update is called once per frame
    void Update()
    {
        timelapse += Time.deltaTime;
        //Debug.Log(timelapse);

        transform.position = startEnemy.transform.position;

        //時間経過で弾を発射する
        if (timelapse >= shoottime && timelapse < returntime)
        {
            LongAttack.enabled = true;
            //Debug.Log("shoot");
            transform.position = Vector3.MoveTowards(startEnemy.transform.position, target.transform.position, speed);
            Invoke("switchTrigger",0.5f);
        }

        //時間経過で元の位置に戻る
        if(timelapse >= returntime)
        {
            ReturnPosition();
        }
    }

    //プレイヤーに向かって進む
    /*private void Moveto()
    {
        LongAttack.enabled = true;
        bullet_collider.isTrigger = true;
        transform.position = Vector3.MoveTowards(startEnemy.transform.position, target.transform.position, speed);
        Invoke("ReturnPosition",2.0f);
    }*/

    //プレイヤーか壁に衝突したら消えて元の位置に戻る
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.CompareTag("Wall"))
        {
            ReturnPosition();
        }
    }

    //元の位置に戻る処理
    private void ReturnPosition()
    {
        //Debug.Log("return");
        LongAttack.enabled = false;
        switchTrigger();
        transform.position = startEnemy.transform.position;
        timelapse = 0.0f;
    }

    //当たり判定を切り替える
    private void switchTrigger()
    {
        if (!bullet_collider.isTrigger)
        {
            bullet_collider.isTrigger = true;
        }

        if (bullet_collider.isTrigger)
        {
            bullet_collider.isTrigger = false;
        }
    }

}
