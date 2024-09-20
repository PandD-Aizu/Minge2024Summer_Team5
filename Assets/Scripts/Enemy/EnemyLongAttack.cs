using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLongAttack : MonoBehaviour
{
    [SerializeField] GameObject target;
    private Vector3 startPos;
    private Vector3 rightshoot;
    private Vector3 leftshoot;
    private Quaternion rot;
    [SerializeField] private float power = 500;
    private float shoottime = 3.0f;
    private float timelapse;
    [SerializeField] GameObject bulletPrefab;
    private GameObject bullet;
    private bool shoot = false;

    // Start is called before the first frame update
    void Start()
    {

    }
 
    // Update is called once per frame
    void Update()
    {
        //弾の生成座標の設定
        startPos = this.transform.position;
        rightshoot = new Vector3(1.0f, 0.0f, 0.0f);
        leftshoot = new Vector3(-1.0f, 0.0f, 0.0f);

        timelapse += Time.deltaTime;

        //時間経過で弾を発射する
        if (timelapse >= shoottime)
        {
            Enemy enemy = GetComponent<Enemy>();

            if (!shoot)
            {
                shoot = true;
                //右に生成して打ち出す
                if ((enemy.movingRight) == true)
                {
                    rot = Quaternion.Euler(0f, 0f, -90f);
                    bullet = Instantiate(bulletPrefab, this.gameObject.transform.position + rightshoot, rot);
                    bullet.GetComponent<Rigidbody>().AddForce(Vector3.right * power);
                }

                //左に生成して打ち出す
                if ((enemy.movingRight) == false)
                {
                    rot = Quaternion.Euler(0f, 0f, 90f);
                    bullet = Instantiate(bulletPrefab, this.gameObject.transform.position + leftshoot, rot);
                    bullet.GetComponent<Rigidbody>().AddForce(Vector3.left* power);
                }

                timelapse = 0.0f;
                shoot = false;
            }
        }
    }

    //プレイヤーか壁に衝突したら消える
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Wall"))
        {
            Destroy(bullet.gameObject);
            shoot = false;
        }
    }

}
