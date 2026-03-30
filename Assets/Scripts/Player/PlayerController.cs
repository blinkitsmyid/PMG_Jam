using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;



[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioSource footstepAudio;
    [SerializeField] private float stepInterval = 0.5f; // интервал между шагами
    private float stepTimer = 0f;
    public static PlayerController Instance { get; private set; }
    public Vector2 GetInputVector() => _inputVector;
    [SerializeField] public float movingSpeed = 10f;
    [Space(20)]
    private LampSwitch _currentSwitch;
    private Door _currentDoor;
    private Vector2 _inputVector;
    private bool availableRun = false;
    private Rigidbody2D _rb;
    //private KnockBack _knockBack;
    private readonly float _minMovingSpeed = 0.1f;
    private bool _isRunning = false;
    private int _keysCollected = 0;   
    private bool _isAlive;
    private float _initialMovingSpeed;
    private bool _isBusy = false;
    private Camera _mainCamera;
    private bool _hasUsedToilet = false;
    [SerializeField] private float runMultiplier = 2f;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D flashlight;
    
    [SerializeField] public Sprite toiletSprite;
    [SerializeField] public Sprite towelSprite;
    [SerializeField] public Sprite keySprite;
    [SerializeField] public Sprite lightSprite;
    private bool _isRunningInput = false;
    private bool _hasKey = false;

   
    private void Awake()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();
        //_knockBack = GetComponent<KnockBack>();

        _mainCamera = Camera.main;
        
        _initialMovingSpeed  = movingSpeed;
    }

    private void Start()
    {
        _isAlive = true;
        _hasUsedToilet = false;
        GameInput.Instance.EnableMovement();
        //GameInput.Instance.OnFlashlightToggle += GameInput_OnFlashlightToggle;
        GameInput.Instance.OnLampInteract += GameInput_OnLampInteract;
        GameInput.Instance.OnDoorInteract += GameInput_OnDoorInteract;
        GameInput.Instance.OnRunStarted += GameInput_OnRunStarted;
        GameInput.Instance.OnRunCanceled += GameInput_OnRunCanceled;
    }
    
    private void Update()
    {
        _inputVector = GameInput.Instance.GetMovementVector();
        availableRun = LevelManager.Instance.CanRun();
        
        if (!_isAlive || _isBusy) return;

        
        
    }
  
    private void OnDestroy()
    {
        /*if (GameInput.Instance != null)
        {
            GameInput.Instance.OnFlashlightToggle -= GameInput_OnFlashlightToggle;
        }
        */
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnLampInteract -= GameInput_OnLampInteract;
        }
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnDoorInteract -= GameInput_OnDoorInteract;
        }
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnRunStarted -= GameInput_OnRunStarted;
            GameInput.Instance.OnRunCanceled -= GameInput_OnRunCanceled;
        }
    }

    private void FixedUpdate()
    {
        if (!_isAlive || _isBusy) return;

      
        HandleMovement();
    }

    public bool IsAlive() => _isAlive;
    

    public void Death()
    {
        PanelManager.Instance.Lose();
    }
    
    public bool IsRunning()
    {
        return _isRunning;
    }
    public bool IsActuallyRunning()
    {
        return _isRunningInput && LevelManager.Instance.CanRun();
    }
    private void HandleMovement()
    {
        float speed = movingSpeed;

        if (_isRunningInput && LevelManager.Instance.CanRun())
        {
            speed *= runMultiplier;
        }

        _rb.MovePosition(_rb.position + _inputVector * (speed * Time.fixedDeltaTime));

        // Определяем движение
        if (Mathf.Abs(_inputVector.x) > _minMovingSpeed || Mathf.Abs(_inputVector.y) > _minMovingSpeed)
        {
            _isRunning = true;
        }
        else
        {
            _isRunning = false;
        }

        // Звуки шагов
        if (_isRunning)
        {
            stepTimer -= Time.fixedDeltaTime;
            if (stepTimer <= 0f)
            {
                footstepAudio.Play();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
    private void GameInput_OnRunStarted(object sender, EventArgs e)
    {
        _isRunningInput = true;
    }

    private void GameInput_OnRunCanceled(object sender, EventArgs e)
    {
        _isRunningInput = false;
    }
    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = _mainCamera.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }
    public void DoToiletRoutine(float duration)
    {
        if (_isBusy) return;
        
        StartCoroutine(ToiletRoutine(duration));
        
    }
    public void GiveKey()
    {
        _keysCollected++;
        Debug.Log($"Keys: {_keysCollected}");
    }

    public int GetKeys()
    {
        return _keysCollected;
    }
    private IEnumerator ToiletRoutine(float duration)
    {
        _isBusy = true;

        // остановить движение
        _inputVector = Vector2.zero;

        // скрыть игрока
        SetVisible(false);

        // можно отключить инпут полностью
        GameInput.Instance.DisableMovement();
        
        Bumble.Instance.ShowBumble(toiletSprite);
        yield return new WaitForSeconds(duration);
       
        Bumble.Instance.HideBumble();
        // вернуть всё обратно
        SetVisible(true);
        GameInput.Instance.EnableMovement(); // если есть Enable()
        _hasUsedToilet = true;
        _isBusy = false;
    }
    
    public bool HasUsedToilet()
    {
        return _hasUsedToilet;
    }
    
    /* private void GameInput_OnFlashlightToggle(object sender, EventArgs e)
    {
        _isFlashlightOn = !_isFlashlightOn;
        flashlight.enabled = _isFlashlightOn;
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Light"))
        {
            HintUI.Instance.ShowTemporary(HintMessages.PressLightOn);
            if (collision.TryGetComponent(out LampSwitch lampSwitch))
            {
                _currentSwitch = lampSwitch;
            }
            
        }
        if (collision.TryGetComponent(out Door door))
        {
            _currentDoor = door;
        }
    }
    private void GameInput_OnLampInteract(object sender, EventArgs e)
    {
        if (_currentSwitch != null)
        {
            _currentSwitch.Interact();
        }
    }
   

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Door door))
        {
            if (_currentDoor == door)
                _currentDoor = null;
        }
        if (collision.TryGetComponent(out LampSwitch lampSwitch))
        {
           if (_currentSwitch == lampSwitch)
               _currentSwitch = null;
        }
    }
    private void GameInput_OnDoorInteract(object sender, EventArgs e)
    {
        if (_currentDoor != null)
        {
            _currentDoor.Interact();
        }
    }
    public void SetVisible(bool visible)
    {
        if (playerSprite != null)
            playerSprite.enabled = visible;

        if (flashlight != null)
            flashlight.enabled = visible;
    }
    
}
