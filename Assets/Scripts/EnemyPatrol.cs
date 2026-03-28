using UnityEngine;

public class EnemyHorizontal : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float leftX = -5f;
    [SerializeField] private float rightX = 5f;
    
    private Vector3 _direction = Vector3.right;

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        transform.position += _direction * patrolSpeed * Time.deltaTime;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        // вправо дошёл → влево
        if (transform.position.x >= rightX)
        {
            _direction = Vector3.left;
            spriteRenderer.flipX = false;
        }
        // влево дошёл → вправо
        else if (transform.position.x <= leftX)
        {
            _direction = Vector3.right;
            spriteRenderer.flipX = true;
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