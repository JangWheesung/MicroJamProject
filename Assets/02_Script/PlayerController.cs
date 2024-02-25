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
    private bool _isTouchingWall = false; // ���� ��Ҵ��� ���� �߰�

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
        // ���� �پ����� ���� ���� ĳ���͸� �����̰� �մϴ�.
        if (!_isAttacking && !_isTouchingWall)
            _rigid.velocity = new Vector2(_moveInput.x * _moveSpeed, _rigid.velocity.y);

        // ���� �پ����� �� ���� �ӵ��� �ٿ��� ������ �ö��� ���ϰ� �մϴ�.
        if (_isTouchingWall)
        {
            // ���� ����� �� ���� �ӵ��� ���̰ų�, ���߰� �ϰ� ������ �Ʒ� �ڵ带 �����ϼ���.
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
                // �� ���� �ӵ��� ������ŵ�ϴ�.
                float wallJumpDirection = _isFacingRight ? -1f : 1f;
                // ���⼭ _jumpImpulse�� �� ū ������ �����ϸ� �� Ÿ�� �ӵ��� �����մϴ�.
                _rigid.velocity = new Vector2(_dashSpeed * wallJumpDirection, _dashSpeed);
                _isTouchingWall = false; // ������ �������� ���� ���� �ʾҴٰ� ǥ���մϴ�.
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
            // ���� �ִϸ��̼� �̺�Ʈ�� ���� ó�� �߰�
            
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
            // õ��� ���� �پ� �ִ� ���¿��� ó���� ���� �߰�
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.05f);
        FinishAttack();
    }
}
