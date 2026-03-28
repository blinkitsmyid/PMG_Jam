using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    //private static readonly int Die = Animator.StringToHash(IsDie);
    //private static readonly int Running = Animator.StringToHash(IsRunning);
    //private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    //private FlashBlink _flashBlink;

    private const string IsRunning = "IsRunning";
    private const string IsDie = "IsDie";
    [SerializeField]private Sprite leftSprite;
    [SerializeField]private Sprite rightSprite;
    [SerializeField]private Sprite upSprite;
    [SerializeField]private Sprite downSprite;
    private void Awake()
    {
        //_animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_flashBlink = GetComponent<FlashBlink>();
    }

    private void Start()
    {
        //Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        //_animator.SetBool(Die, true);
        //_flashBlink.StopBlinking();
    }

    private void Update()
    {
        //_animator.SetBool(Running, Player.Instance.IsRunning());
        AdjustPlayerFacingDirection();
    }
    private void AdjustPlayerFacingDirection()
    {
        Vector2 dir = GameInput.Instance.GetMovementVector();

        if (dir == Vector2.zero) return;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            // горизонталь
            if (dir.x > 0)
                _spriteRenderer.sprite = rightSprite;
            else
                _spriteRenderer.sprite = leftSprite;
        }
        else
        {
            // вертикаль
            if (dir.y > 0)
                _spriteRenderer.sprite = upSprite;
            else
                _spriteRenderer.sprite = downSprite;
        }
    }
    private void OnDestroy()
    {
        
    }
}