using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// プレイヤーが発射する弾を制御するコンポーネント
public class Shot : MonoBehaviour
{
    private Vector3 m_velocity; // 速度
    Rigidbody rb;
    [SerializeField] Player Player;
    // 毎フレーム呼び出される関数

    private void Start()
    {
        Player = GameObject.FindAnyObjectByType<Player>();
         rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        // 移動する
        transform.localPosition += m_velocity;
    }

    // 弾を発射する時に初期化するための関数
    public void Init(float angle, float speed)
    {
        // 弾の発射角度をベクトルに変換する
        var direction = Utils.GetDirection(angle);

        // 発射角度と速さから速度を求める
        m_velocity = direction * speed;

        // 弾が進行方向を向くようにする
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;

        // 2 秒後に削除する
        Destroy(gameObject, 5);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.recreaLife(Player.damage);
            Destroy(this.gameObject);
          //  Debug.Log("Destroyed");
        }

        if (other.gameObject.CompareTag("Wall")|| other.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
            //  Debug.Log("Destroyed");
        }
    }
}
