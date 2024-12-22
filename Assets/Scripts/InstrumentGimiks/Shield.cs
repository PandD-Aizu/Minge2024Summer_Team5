using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Shield : MonoBehaviour
{
    public float knockbackForce;
    public GameObject m_shotPrefab2;
    public GameObject boss;
    [SerializeField] private Renderer Dshield;
    [SerializeField] private Animator Animator;
    [SerializeField] public bool Shield_playing;
    public float moveSpeed = 2.0f; // 移動速度を指定

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss");
        knockbackForce = 5f;
        Dshield.enabled = true;
        Shield_playing = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Rキーを押すとシールド発動
        if (Input.GetKey(KeyCode.R) && !Shield_playing)
        {
            Dshield.enabled = true;
            Animator.SetBool("shieldPlay", true);
            Shield_playing = true;
            Invoke("RenderFalse", 5.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet")) { 
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Reflectable"))
        {
            Vector3 current = other.transform.position;
            Quaternion rot = transform.localRotation; // プレイヤーの向き

            Destroy(other.gameObject); // 元のオブジェクトを削除

            Debug.Log("反射！");
            GameObject shot = Instantiate(m_shotPrefab2, current, rot);

            shot.tag = "refrectBullet";

            // コルーチンを使ってスムーズにボスの位置に移動させる
            StartCoroutine(MoveToTarget(shot, boss.transform.position));
        }
    }

    // ボスの位置までオブジェクトをスムーズに移動させるコルーチン
    IEnumerator MoveToTarget(GameObject obj, Vector3 targetPosition)
    {
        while (Vector3.Distance(obj.transform.position, targetPosition) > 0.1f)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    //攻撃が当たった場合にノックバックを適用
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            ApplyKnockback(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }

    //数秒後にシールド解除
    public void RenderFalse()
    {
        Dshield.enabled = false;
        Animator.SetBool("shieldPlay", false);
        //Invoke("CoolTime", 5.0f);
        Debug.Log("クールタイム開始いいいいいいい");
    }

    //クールタイム
    public void CoolTime()
    {
        Shield_playing = false;
        Debug.Log("クールタイム終わり");
    }

    //攻撃を与えた相手にノックバックを適用する関数
    public void ApplyKnockback(GameObject target)
    {
        Rigidbody targetRb = target.GetComponent<Rigidbody>();
        if (targetRb != null)
        {
            Vector3 knockbackDirection = (target.transform.position - transform.position).normalized;
            targetRb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
    }
}
