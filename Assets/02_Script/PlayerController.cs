using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigid;
    BoxCollider2D _box;
    SpriteRenderer _sp;
    Animator _animator;

    [SerializeField] private PlayerSmashingEffect playerSmashingEffect;
    [SerializeField] private Slice playerSlice;

    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _jumpImpulse = 5f;
    [SerializeField] private float _dashSpeed = 5f;

    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashCooldown = 1f;

    public bool _isGrounded = false;
    private bool _doubleJumped = false;
    private bool _isDashing = false;
    private bool _isAttacking = false;
    private bool _possibleDashing = true;
    private bool _isTouchingWall = false; // 벽에 닿았는지 여부 추가

    private Vector2 _moveInput;

    [SerializeField]
    private bool _isFacingRight = true;
    public bool isFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);

                //transform.localScale = new Vector3(value ? 1 : -1, 1, 1);
            }
            _isFacingRight = value;
        }
    }

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
        _sp = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        TimeSystem.Instance.OnGameoverEvt += DeathPlayer;
    }

    private void DeathPlayer()
    {
        Slice slice = PoolingManager.instance.Pop<Slice>(playerSlice.name, transform.position);
        slice.BreakEffect();

        _sp.enabled = false;
        enabled = false;
    }

    void FixedUpdate()
    {
        if(!_isDashing)
            _rigid.velocity = new Vector2(_moveInput.x * _moveSpeed, _rigid.velocity.y);
    }


    private void Update()
    {
        SetFacingDirection(_moveInput);
        Attack();
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            isFacingRight = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_isGrounded || (!_isGrounded && !_doubleJumped))
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpImpulse);
                if (!_isGrounded && !_doubleJumped)
                {
                    _doubleJumped = true;
                }
                _isGrounded = false;
            }
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && _possibleDashing)
        {
            StartCoroutine(Dash());
        }
    }

    public void Attack()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && !_isAttacking)
        {
            _isAttacking = true;
            StartCoroutine(AttackDelay());

            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));

            Vector3 attackVec = point - transform.position;
            attackVec.Normalize();
            Vector3 effectPosition = transform.position + attackVec * 2f;
            
            PlayerSmashingEffect effect = PoolingManager.instance.Pop<PlayerSmashingEffect>(playerSmashingEffect.name, effectPosition);
            effect.transform.up = -attackVec;
            effect.EnemyHit();

            AttackEffectSystem.Instance.CinemachineShaking(null);
            AttackEffectSystem.Instance.SoundEffect(null);
        }
    }

    private void FinishAttack()
    {
        _isAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _doubleJumped = false;
        }
    }

    private IEnumerator Dash()
    {
        _isDashing = true;
        _possibleDashing = false;

        // 대시하는 동안의 이동 속도 설정
        Vector2 dashVelocity = new Vector2(_isFacingRight ? _dashSpeed : -_dashSpeed, _rigid.velocity.y);
        _rigid.velocity = dashVelocity;

        // 대시 지속 시간만큼 대기
        yield return new WaitForSeconds(_dashDuration);

        // 대시 끝난 후 속도 초기화
        _rigid.velocity = Vector2.zero;
        _isDashing = false;

        // 대시 쿨다운 적용
        yield return new WaitForSeconds(_dashCooldown);

        _possibleDashing = true;
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.25f);
        FinishAttack();
    }
}
