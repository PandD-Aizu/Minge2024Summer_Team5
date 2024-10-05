using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RshotDestroyed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            if (other.gameObject.GetComponent<BossEnemy>().isAttacking) {
                other.gameObject.GetComponent<BossEnemy>().TakeDamage();
            }
            
            Debug.Log("オブジェクトを破壊");
            Destroy(this.gameObject);
        }
    }
}
