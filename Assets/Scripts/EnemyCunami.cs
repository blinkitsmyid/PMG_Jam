using UnityEngine;

public class EnemyCunami : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxChaseDistance = 15f;

    private Vector3 _spawnPosition;
    private bool _isFrozen = false;
    private bool _isFollowing = false;
    private bool _isLampFollowing = false;
    
    private Transform _targetLamp;

    private void Start()
    {
        speed = PlayerController.Instance.movingSpeed;
        _spawnPosition = transform.position;
    }
    private void Update()
    {
        
        if (PlayerController.Instance == null) return;
        if (_isFrozen) return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);

        // 💡 Приоритет лампы
        if (_isLampFollowing)
        {
            FollowLamp();
            return;
        }

        // 🏃 Преследование игрока
        if (_isFollowing)
        {
            if (distanceToPlayer <= maxChaseDistance)
            {
                FollowPlayer();
            }
            else
            {
                _isFollowing = false;
            }
        }
        else
        {
            ReturnToSpawn();
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // ☠️ убивает игрока
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
                _isFrozen = true;
                _isFollowing = false;
                _isLampFollowing = false;
                _targetLamp = null;
            }
        }

       
        if (collision.TryGetComponent(out Flashlight flashlight) && !_isLampFollowing)
        {
            _isFollowing = true;
        }
    }
    
    private void FollowPlayer()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 direction = (playerPos - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;
    }
    private void FollowLamp()
    {
        if (_targetLamp == null) return;

        Vector3 direction = (_targetLamp.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, _targetLamp.position);

        if (distance > 0.3f) 
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
    private void ReturnToSpawn()
    {
        Vector3 direction = (_spawnPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, _spawnPosition);

        if (distance > 0.1f)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}