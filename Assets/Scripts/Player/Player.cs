using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 6f;
    public int jumppower = 250; // ジャンプする力
    public bool isGround;
    public GameObject respornPoint;
    private Rigidbody rb;

    // gravityControllerを参照するための変数
    private gravityController gravityCtrl;

    void Start()
    {
        respornPoint = GameObject.Find("respornPoint");
        rb = GetComponent<Rigidbody>();
        isGround = false;

        // gravityControllerスクリプトを取得。nullチェックを行う
        gravityCtrl = FindObjectOfType<gravityController>();

        // gravityControllerが見つからなかった場合、エラーメッセージを表示
        if (gravityCtrl == null)
        {
            Debug.LogWarning("gravityControllerが見つかりません。");
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Dragable")
        {
            isGround = true;
        }
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
        rb.velocity = new Vector3(0, 0, 0);
        this.transform.position = respornPoint.transform.position;
    }
}

