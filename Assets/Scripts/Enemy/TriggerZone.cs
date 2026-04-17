using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public EnemyCunami enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.StartChase();
        }
    }
}