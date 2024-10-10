using UnityEngine;

public class IgnoreEnemyCollision : MonoBehaviour
{
    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            for (int j = i + 1; j < enemies.Length; j++)
            {
                Collider collider1 = enemies[i].GetComponent<Collider>();
                Collider collider2 = enemies[j].GetComponent<Collider>();

                if (collider1 != null && collider2 != null)
                {
                    Physics.IgnoreCollision(collider1, collider2);
                }
            }
        }
    }
}
