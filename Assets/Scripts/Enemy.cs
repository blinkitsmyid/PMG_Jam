using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _patrolSpeed = 1f;
    [SerializeField] private float topY = 5f;      // верхняя граница
    [SerializeField] private float bottomY = -5f;  // нижняя граница

    private Vector3 _direction = Vector3.up;

    protected virtual void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        transform.position += _direction * _patrolSpeed * Time.deltaTime;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // если дошёл до верхней точки → идёт вниз
        if (transform.position.y >= topY)
        {
            _direction = Vector3.down;
            spriteRenderer.flipY = false;
        }
        // если дошёл до нижней → идёт вверх
        else if (transform.position.y <= bottomY)
        {
            _direction = Vector3.up;
            spriteRenderer.flipY = true;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
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