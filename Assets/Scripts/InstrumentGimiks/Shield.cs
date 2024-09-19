using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    [SerializeField] private Renderer Dshield;
    [SerializeField] private Animator Animator;
    [SerializeField] private bool Shield_playing;
    public float knockbackForce;

    // Start is called before the first frame update
    void Start()
    {

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
        Invoke("CoolTime", 5.0f);
    }

    //クールタイム
    public void CoolTime()
    {
        Shield_playing = false;
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
