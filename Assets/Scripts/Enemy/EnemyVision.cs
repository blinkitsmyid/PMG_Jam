using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public EnemyLooker enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.StartChase();
        }
    }
}