using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        // Player‚ÆÚG‚µ‚½‚©‚Ç‚¤‚©‚ğŠm”F
        if (collision.gameObject.CompareTag("Player"))
        {
            // Player‚ÌRigidbody‚ğæ“¾
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            // Player‚ª“G‚Ìã‚©‚ç“¥‚ñ‚¾‚©‚Ç‚¤‚©‚ğŠm”F
            if (playerRb != null && IsPlayerAbove(collision))
            {
                // “G‚ğÁ‚·ˆ—
                Destroy(gameObject);
            }
        }
    }

    // Player‚ª“G‚Ìã‚©‚ç“¥‚ñ‚¾‚©‚Ç‚¤‚©‚ğ”»’è‚·‚éŠÖ”
    private bool IsPlayerAbove(Collision collision)
    {
        // Player‚Ì‘«Œ³‚ÌˆÊ’u
        Vector3 playerFeetPosition = collision.transform.position - new Vector3(0, collision.transform.localScale.y / 2, 0);
        Debug.Log(playerFeetPosition);

        // “G‚Ì“ª‚ÌˆÊ’u
        Vector3 enemyTopPosition = transform.position + new Vector3(0, transform.localScale.y / 2, 0);
        Debug.Log("enemy :" +enemyTopPosition);

        // Player‚ª“G‚Ìã•û‚©‚çÚG‚µ‚½‚©‚ğ”»’è
        return playerFeetPosition.y - enemyTopPosition.y <0.1;
    }
}
