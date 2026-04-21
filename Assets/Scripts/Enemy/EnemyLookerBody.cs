using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.TryGetComponent(out PlayerController player))
        {
            player.Death();
        }

        // свет лампы → респавн врага
        if (collision.TryGetComponent(out Lamp lamp))
        {
            if (lamp.IsOn())
            {
                Debug.Log("Lamp On");
                GetComponentInParent<EnemyLooker>().Respawn();
            }
        }
    }
}