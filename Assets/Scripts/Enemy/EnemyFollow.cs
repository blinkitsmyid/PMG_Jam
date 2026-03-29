using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float speed;

    private enum State
    {
        Idle,
        ChasePlayer,
        GoToLamp
    }

    private State _state = State.Idle;

    private bool _hasSeenPlayer = false;
    private Transform _targetLamp;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        speed = PlayerController.Instance.movingSpeed;
    }

    private void Update()
    {
        if (PlayerController.Instance == null) return;

        Vector3 moveDirection = Vector3.zero;

        switch (_state)
        {
            case State.Idle:
                break;

            case State.ChasePlayer:
                moveDirection = FollowPlayer();
                break;

            case State.GoToLamp:
                moveDirection = FollowLamp();
                break;
        }

        UpdateAnimation(moveDirection);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 🔦 активируется один раз
        if (!_hasSeenPlayer && collision.GetComponentInParent<Flashlight>() != null)
        {
            _hasSeenPlayer = true;
            _state = State.ChasePlayer;
        }

        // 💡 лампа имеет приоритет
        if (collision.TryGetComponent(out Lamp lamp))
        {
            if (lamp.IsOn())
            {
                _state = State.GoToLamp;
                _targetLamp = lamp.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Lamp lamp))
        {
            _targetLamp = null;

            if (_hasSeenPlayer)
                _state = State.ChasePlayer;
            else
                _state = State.Idle;
        }
    }

    private Vector3 FollowPlayer()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 direction = (playerPos - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;
        return direction;
    }

    private Vector3 FollowLamp()
    {
        if (_targetLamp == null)
        {
            _state = State.ChasePlayer;
            return Vector3.zero;
        }

        Lamp lamp = _targetLamp.GetComponent<Lamp>();

        // если лампу выключили → обратно к игроку
        if (lamp == null || !lamp.IsOn())
        {
            _targetLamp = null;
            _state = State.ChasePlayer;
            return Vector3.zero;
        }

        Vector3 direction = (_targetLamp.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, _targetLamp.position);

        if (distance > 0.3f)
        {
            transform.position += direction * speed * Time.deltaTime;
            return direction;
        }

        return Vector3.zero; // дошёл → стоит
    }

    private void UpdateAnimation(Vector3 direction)
    {
        bool isMoving = direction.magnitude > 0.05f;
        _animator.SetBool("IsMoving", isMoving);

        if (!isMoving)
        {
            _animator.SetInteger("Direction", 0);
            return;
        }

        int dir = 0;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            dir = 1; // Right
            _spriteRenderer.flipX = direction.x < 0;
        }
        else
        {
            dir = 2; // Down
        }

        _animator.SetInteger("Direction", dir);

        Debug.Log($"SET DIR: {dir} | direction: {direction}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            player.Death();
        }
    }
}