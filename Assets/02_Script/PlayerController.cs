using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigid;
    BoxCollider2D _box;
    Animator _animator;

    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _jumpImpulse = 5f;
    [SerializeField] private float _dashSpeed = 5f;

    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashCooldown = 1f;

    public bool _isGrounded = false;
    private bool _doubleJumped = false;
    private bool _isDashing = false;
    private bool _isAttacking = false;
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
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // 벽에 붙어있지 않을 때만 캐릭터를 움직이게 합니다.
        if (!_isAttacking && !_isTouchingWall)
            _rigid.velocity = new Vector2(_moveInput.x * _moveSpeed, _rigid.velocity.y);

        // 벽에 붙어있을 때 수직 속도를 줄여서 빠르게 올라가지 못하게 합니다.
        if (_isTouchingWall)
        {
            // 벽에 닿았을 때 수직 속도를 줄이거나, 멈추게 하고 싶으면 아래 코드를 조정하세요.
            _rigid.velocity = new Vector2(_rigid.velocity.x, Mathf.Max(_rigid.velocity.y, 0));
        }
    }


    private void Update()
    {
        if (!_isAttacking)
        {
            SetFacingDirection(_moveInput);
            Attack();
        }
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
            else if (_isTouchingWall && !_isGrounded)
            {
                // 벽 점프 속도를 증가시킵니다.
                float wallJumpDirection = _isFacingRight ? -1f : 1f;
                // 여기서 _jumpImpulse를 더 큰 값으로 조정하면 벽 타기 속도가 증가합니다.
                _rigid.velocity = new Vector2(_dashSpeed * wallJumpDirection, _dashSpeed);
                _isTouchingWall = false; // 벽에서 떨어지면 벽에 닿지 않았다고 표시합니다.
            }
        }
    }



    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && !_isDashing && !_isAttacking)
        {
            _isDashing = true;
            float dashDirection = isFacingRight ? 1 : -1;
            _rigid.velocity = new Vector2(dashDirection * _dashSpeed, _rigid.velocity.y);
            Invoke("ResetDash", _dashDuration);
            Invoke("ResetDashState", _dashCooldown);
        }
    }

    private void ResetDash()
    {
        _rigid.velocity = Vector2.zero;
    }

    private void ResetDashState()
    {
        _isDashing = false;
    }

    public void Attack()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && !_isAttacking)
        {
            _isAttacking = true;
            _animator.SetTrigger("Attack");
            // 공격 애니메이션 이벤트에 대한 처리 추가
            
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
        else if (collision.gameObject.CompareTag("Wall"))
        {
            _isTouchingWall = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _isTouchingWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _isTouchingWall = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ceiling") && _isTouchingWall)
        {
            // 천장과 벽에 붙어 있는 상태에서 처리할 로직 추가
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.05f);
        FinishAttack();
    }
}
