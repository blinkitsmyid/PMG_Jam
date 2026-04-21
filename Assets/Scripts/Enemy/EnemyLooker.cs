using UnityEngine;

public class EnemyLooker : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float chaseSpeedMultiplier = 1.5f;
    [SerializeField] private float patrolDistance = 3f;
    [SerializeField] private bool isHorizontal = true;

    private Vector3 _startPos;
    private bool _isFollowing = false;
    private float _patrolTimer;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        _startPos = transform.position;
    }

    private void Update()
    {
        if (PlayerController.Instance == null) return;

        Vector3 dir = Vector3.zero;

        if (_isFollowing)
        {
            dir = (PlayerController.Instance.transform.position - transform.position).normalized;
            transform.position += dir * speed * chaseSpeedMultiplier * Time.deltaTime;
        }
        else
        {
            dir = Patrol();
        }

        UpdateAnimation(dir);
    }

    private Vector3 Patrol()
    {
        float move = Mathf.Sin(Time.time * speed);
        Vector3 offset;

        if (isHorizontal)
            offset = new Vector3(move * patrolDistance, 0, 0);
        else
            offset = new Vector3(0, move * patrolDistance, 0);

        Vector3 target = _startPos + offset;
        Vector3 dir = (target - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;

        return dir;
    }

    public void StartChase()
    {
        _isFollowing = true;
    }

    public void Respawn()
    {
        Debug.Log("Respawn");
        transform.position = _startPos;
        _isFollowing = false;
    }

    private void UpdateAnimation(Vector3 direction)
    {
        bool isMoving = direction.magnitude > 0.05f;
        _animator.SetBool("IsMoving", isMoving);

        if (!isMoving) return;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            _animator.SetInteger("Direction", 1);
            _spriteRenderer.flipX = direction.x < 0;
        }
        else
        {
            _animator.SetInteger("Direction", 2);
        }
    }
}