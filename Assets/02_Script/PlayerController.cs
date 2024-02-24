using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigid;
    BoxCollider2D _box;

    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _jumpImpulse = 5f;
    [SerializeField] private float _dashSpeed = 5f;

    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashCooldown = 1f;

    private bool _isGrounded;
    private bool _doubleJumped = false;
    private bool _isDashing = false;

    private Vector2 _moveInput;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        _rigid.velocity = new Vector2(_moveInput.x * _moveSpeed, _rigid.velocity.y);
    }

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
            }
            _isFacingRight = value;
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
        Vector2 movement = new Vector2(Keyboard.current.dKey.isPressed ? 1 : Keyboard.current.aKey.isPressed ? -1 : 0, 0);
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        //TODO Check if alice as well
        if (context.started && !_isGrounded)
        {
            _isGrounded = false;
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpImpulse);
        }
        else if(context.started && !_isGrounded)
        {
            _doubleJumped = true;
        }
        else if(context.started && _doubleJumped)
        {
            return;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        _isDashing = true;
        _rigid.velocity = new Vector2(_rigid.velocity.x, 0f); // 대쉬 중에 수직 이동 방지

        if (Keyboard.current.dKey.isPressed)
        {
            _rigid.AddForce(Vector2.right * _dashSpeed, ForceMode2D.Impulse);
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            _rigid.AddForce(Vector2.left * _dashSpeed, ForceMode2D.Impulse);
        }

        Invoke("ResetDash", _dashDuration);
        Invoke("ResetDashState", _dashCooldown);
    }

    private void ResetDash()
    {
        _rigid.velocity = Vector2.zero;
    }

    private void ResetDashState()
    {
        _isDashing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

}
