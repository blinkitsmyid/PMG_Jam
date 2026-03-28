using System;
using UnityEngine;

public class EnemyLooker : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxChaseDistance = 15f;
    [SerializeField] private Transform visionLight; 
    
    private Vector3 _spawnPosition;

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

        Vector3 moveDirection = Vector3.zero;
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);

       
        if (_isLampFollowing)
        {
            moveDirection = (_targetLamp.position - transform.position).normalized;
            FollowLamp();
        }
        
        else if (_isFollowing)
        {
            if (distanceToPlayer <= maxChaseDistance)
            {
                moveDirection = (PlayerController.Instance.transform.position - transform.position).normalized;
                FollowPlayer();
            }
            else
            {
                _isFollowing = false;
            }
        }
        
        else
        {
            moveDirection = (_spawnPosition - transform.position).normalized;
            Respawn();
        }
        
        if (visionLight != null && moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            visionLight.rotation = Quaternion.Euler(0, 0, angle -90);
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Lamp lamp))
        {
            if (lamp.IsOn())
            {
                _isFollowing = false;
                _isLampFollowing = true;
                _targetLamp = lamp.transform;
            }
        }

        if (collision.TryGetComponent(out Flashlight flashlight) && !_isLampFollowing)
        {
            _isFollowing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Lamp lamp))
        {
            _isLampFollowing = false;
            _targetLamp = null;
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
            transform.position += direction * speed * Time.deltaTime;
    }

    public void Respawn()
    {
        transform.position = _spawnPosition;
        _isFollowing = false;
        _isLampFollowing = false;
        _targetLamp = null;
    }
}