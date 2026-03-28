using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out PlayerController player))
        {
            player.Death();
        }
    }
    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Lamp lamp))
        {
            if (lamp.IsOn())
            {
                Die();
            }
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Enemy died from lamp");
        Destroy(gameObject);
    }
}