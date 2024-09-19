using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongAttack : MonoBehaviour
{
    [SerializeField] GameObject target;
    private Vector3 rightshoot;
    private Vector3 leftshoot;
    private float speed = 1.0f;
    private float shoottime = 1.0f;
    private float returntime = 3.0f;
    private float timelapse;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bullet;
    private bool shoot = false;

    // Start is called before the first frame update
    void Start()
    {

    }
 
    // Update is called once per frame
    void Update()
    {
        //弾の生成座標の設定
        rightshoot = new Vector3(1.0f, 0.0f, 0.0f);
        leftshoot = new Vector3(-1.0f, 0.0f, 0.0f);

        timelapse += Time.deltaTime;
        //Debug.Log(timelapse);


        //時間経過で弾を発射する
        if (timelapse >= shoottime && timelapse < returntime)
        {
            Enemy enemy = GetComponent<Enemy>();
            if (!shoot)
            {
                //右に生成
                if (enemy.movingRight)
                {
                    Debug.Log("right");
                    bullet = Instantiate(bulletPrefab, rightshoot, transform.rotation);
                    //bullet.GetComponent<Rigidbody>().AddForce
                }

                //左に生成
                if (!enemy.movingRight)
                {
                    bullet = Instantiate(bulletPrefab, rightshoot, transform.rotation);
                }

                shoot = true;
            }

            if (shoot)
            {
                //右向き移動
                if (enemy.movingRight)
                {
                    bullet.transform.position = new Vector3(speed, 0.0f, 0.0f);
                }

                //左向き移動
                if (!enemy.movingRight)
                {
                    bullet.transform.position = new Vector3(-speed, 0.0f, 0.0f);
                }
            }
                
            
            
        }

        //時間経過で消える
        if(timelapse >= returntime)
        {
            Destroy(bulletPrefab.gameObject);
        }
    }

    //プレイヤーか壁に衝突したら消える
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.CompareTag("Wall"))
        {
            Destroy(bulletPrefab.gameObject);
        }
    }

}
