using UnityEngine;

public class EnemyCunami : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxChaseDistance = 15f;

    private bool _isFrozen = false;
    private bool _isFollowing = false;

    private void Start()
    {
        speed = PlayerController.Instance.movingSpeed * 1.5f; // быстрее игрока
    }

    private void Update()
    {
        if (PlayerController.Instance == null) return;
        if (_isFrozen) return;

        if (_isFollowing)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 direction = (playerPos - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isFrozen) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Death();
        }
        if (collision.gameObject.CompareTag("Lamp"))
        {
            if (GetComponent<Lamp>().IsOn())
            {
                FreezeEnemy();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Lamp lamp))
        {
            if (lamp.IsOn())
            {
                FreezeEnemy();
            }
        }
    }
    public void StartChase()
    {
        if (_isFrozen) return;

        _isFollowing = true;
    }
    private void FreezeEnemy()
    {
        if (_isFrozen) return; // 🔥 защита

        _isFrozen = true;
        _isFollowing = false;

        Debug.Log("Enemy frozen!");
    }
}