using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 6f;
    public int jumppower = 300; // ジャンプする力
    public bool isGround;
    public GameObject respornPoint;
    private Rigidbody rb;
    public int maxhp = 3;
    public int damage = 1;
    public int currenthp = 3;

    public AudioSource playerAudio;
    public AudioClip damegedse; //ダメージを受けた時のSE
    public AudioClip jumpse;    //ジャンプした時のSE
    public AudioClip footstepse;    //足音

    // gravityControllerを参照するための変数
    private gravityController gravityCtrl;
    // SphereMoveを参照するための変数
    private SphereMove SphereMv;
    private ShowHp Showhp;
    private Timer timer;
    private InstrumentManager instrument;
    GameObject instruments;

    private bool isWalking = false;  // プレイヤーが歩いているかどうか
    public float footstepInterval = 0.4f;  // 足音の再生間隔
    private float nextFootstepTime = 0f;   // 次に足音を再生する時刻

    public GameObject Boss;

    void Start()
    {
        Debug.Log(currenthp);
        Showhp = GameObject.FindObjectOfType<ShowHp>();
        Showhp.UpdateHearts(currenthp);
        timer = GameObject.FindObjectOfType<Timer>();
        instrument = GameObject.FindObjectOfType<InstrumentManager>();
        respornPoint = GameObject.Find("respornPoint");
        rb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        isGround = false;   

        // gravityControllerスクリプトを取得。nullチェックを行う
        gravityCtrl = FindObjectOfType<gravityController>();

        // gravityControllerが見つからなかった場合、エラーメッセージを表示
        if (gravityCtrl == null)
        {
            Debug.LogWarning("gravityControllerが見つかりません。");
        }

        // SphereMoveスクリプトを取得。nullチェックを行う
        SphereMv = FindObjectOfType<SphereMove>();

        // SphereMoveが見つからなかった場合、エラーメッセージを表示
        if (SphereMv.ElevatorFloor == null)
        {
            Debug.LogWarning("ElevatorFloorが見つかりません。");
        }
        // Showhp.UpdateHearts(currenthp);

        if (playerAudio == null)
        {
            Debug.LogWarning("playerのAudioSourceが見つかりません。");
        }

        Boss = GameObject.Find("Boss");
        if (Boss == null) {
            Debug.LogWarning("Bossが見つかりません。");
        }

    }

    void Update()
    {
        // 左右の移動入力
        float moveHorizontal = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1f; // Aキーで左移動
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1f; // Dキーで右移動
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // 移動ベクトルの計算
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

        // Rigidbodyを使用して移動
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);

        // プレイヤーが動いているかどうかをチェック
        if (movement.magnitude > 0 && isGround)
        {
            if (!isWalking)
            {
                isWalking = true;
                playerAudio.PlayOneShot(footstepse);  // 足音を即座に再生
                nextFootstepTime = Time.time + footstepInterval;  // 次の足音の時間を設定
            }

            // 足音を再生する
            if (Time.time >= nextFootstepTime)
            {
                playerAudio.PlayOneShot(footstepse);  // 足音の再生
                nextFootstepTime = Time.time + footstepInterval;  // 次の足音の時間を更新
            }
        }
        else
        {
            isWalking = false;  // プレイヤーが動いていない場合
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.name == "SceneChange")
        {
            SceneManager.LoadScene("StageSelectScene");
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Boss" || collision.gameObject.tag == "Reflectable")
        {
            recreaLife(damage);
        }

        if (collision.gameObject.tag == "Abyss")
        {
            recreaLife(damage = 3);
            damage = 1;
        }

        if (collision.gameObject.name == "DrMark")
        {
            instrument.isDrumAvailable = true;
            instrument.InstrumentInit();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "BaMark")
        {
            instrument.isBassAvailable = true;
            instrument.InstrumentInit();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "KeyMark")
        {
            instrument.isPianoAvailable = true;
            instrument.InstrumentInit();
            instruments = timer.Instrument;
            Destroy(instruments);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Dragable")
        {
            isGround = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {       
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Dragable")
        {
            isGround = false;
        }
    }

    public void Jump()
    {
        if (isGround == true)
        {
            playerAudio.PlayOneShot(jumpse);
            // gravityControllerが存在するかどうかを確認
            if (gravityCtrl != null && gravityCtrl.InZoneChecker == 1) // 反転ゾーン内
            {
                // 反転ゾーン内では逆方向にジャンプ
                rb.AddForce(new Vector3(0, -jumppower, 0)); 
            }
            else // 通常ゾーン
            { 
                // 通常のジャンプ
                rb.AddForce(new Vector3(0, jumppower, 0));
            }
        }
    }

    public void playerResporn()
    {
        if (Boss) {
            Boss.GetComponent<BossEnemy>().currentPhase = 0;
            Boss.GetComponent<BossEnemy>().UpdateBossAppearance();
        }
        rb.velocity = new Vector3(0, 0, 0);
        currenthp = maxhp;
        if (timer) {
            timer.remainingTime = timer.totalTime;
        }
        
        this.transform.position = respornPoint.transform.position;
    }

    public void recreaLife(int damage)
    {
        currenthp-=damage;
        playerAudio.PlayOneShot(damegedse);
        Debug.Log(currenthp);

        if (currenthp >= 0)
        {
            Showhp.UpdateHearts(currenthp);
            Showhp.DamagedHearts(damage, currenthp);
        }

        if ( currenthp <= 0 )   
        {
            playerResporn();
            Debug.Log("resporn");
            Showhp.UpdateHearts(currenthp);
        }
    }
}

    


