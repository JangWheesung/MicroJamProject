using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerBase : MonoBehaviour
{
    [Header("Base_Prefab")]
    [SerializeField] protected AttackEffeectBase attackEffect;
    [SerializeField] protected EXEffectBase exEffect;
    [SerializeField] protected Slice playerSlice;

    [Header("Base_Value")]
    [SerializeField] protected float moveSpeed = 9f;
    [SerializeField] protected float jumpPower = 12f;
    [SerializeField] protected int jumpCount = 2;
    [SerializeField] protected float dashSpeed = 23f;
    [SerializeField] protected float dashDuration = 0.2f;
    [SerializeField] protected float dashDelay = 1f;
    [SerializeField] protected float attackDelay = 0.25f;

    protected Rigidbody2D rb;
    protected SpriteRenderer sp;

    protected bool isFacingRight = true;
    protected bool isGrounded = false;
    protected bool isDash = false;
    protected bool isEX;
    protected bool isDead = false;

    protected bool pAttack = true;
    protected bool pDash = true;

    protected int currentJumpCount = 0;
    protected bool isInvincibility = false;

    private TrailRenderer trail;
    private Camera cam;

    [HideInInspector] public Vector2 MovementVector { get; private set; }

    #region LifeCycle

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();

        trail = GetComponentInChildren<TrailRenderer>();
        cam = Camera.main;
    }

    protected virtual void Start() //죽는 이벤트 구독
    {
        GameSystem.Instance.OnEXTriggerEvt += HandleEX;
        TimeSystem.Instance.OnGameoverEvt += HandleDeath;
    }

    protected virtual void FixedUpdate() //리지드바디 연산
    {
        if (!isDash)
        {
            Vector2 moveVec = new Vector2(MovementVector.x * moveSpeed, rb.velocity.y);
            SetRigidbody(moveVec);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            currentJumpCount = 0;
        }
    }

    #endregion

    #region Input

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isDead) return;

        Move(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isDead) return;

        if (context.started)
        {
            Jump();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (isDead) return;

        if (context.started)
        {
            Dash();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (isDead || isEX) return;

        if (context.started)
        {
            Attack();
        }
    }

    public void OnEX(InputAction.CallbackContext context)
    {
        if (isDead) return;

        if (context.started)
        {
            EX();
        }
    }

    #endregion

    #region Virtual_Input

    protected virtual void Move(Vector2 vec)
    {
        MovementVector = vec;
        SetFacingDirection(MovementVector);
    }

    protected virtual void Jump()
    {
        if (currentJumpCount < jumpCount)
        {
            AudioManager.Instance.StartSfx("Jump");

            SetRigidbody(new Vector2(rb.velocity.x, jumpPower));
            currentJumpCount++;

            if (isGrounded)
            {
                isGrounded = false;
            }
        }
    }

    protected virtual void Dash()
    {
        if (!pDash) return;

        AudioManager.Instance.StartSfx($"Dash");

        isDash = true;
        pDash = false;

        Vector2 dashVelocity = new Vector2(isFacingRight ? dashSpeed : -dashSpeed, rb.velocity.y);
        SetRigidbody(dashVelocity);

        StartCoroutine(DashDelay());
    }

    protected virtual void Attack()
    {
        if (!pAttack) return;

        pAttack = false;
        StartCoroutine(AttackDelay());

        AttackEffeectBase effect = PoolingManager.Instance.Pop<AttackEffeectBase>(attackEffect.name, transform.position);
        effect.PopEffect();

        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_S);
    }

    protected void EX()
    {
        EXGaugeBar gaugeBar = FindObjectOfType<EXGaugeBar>();
        if (!gaugeBar.IsCharging) return;

        StartCoroutine(EXEvent());
    }

    #endregion

    #region Virtual_Event

    public virtual void Hit()
    {
        if (isInvincibility) return;

        TimeSystem.Instance.MinusTime(2);
    }

    protected virtual void Death()
    {
        Slice slice = PoolingManager.Instance.Pop<Slice>(playerSlice.name, transform.position);
        slice.BreakEffect();

        sp.enabled = false;
        trail.enabled = false;
        enabled = false;
    }

    #endregion

    #region Handle

    private void HandleEX(bool value)
    {
        isEX = value;

        if (value)
        {
            rb.velocity = Vector2.zero;

            EXEffectBase effect = PoolingManager.Instance.Pop<EXEffectBase>(exEffect.name, transform.position);
            effect.PopEffect();
        }
    }

    private void HandleDeath()
    {
        StartCoroutine(DeathEvent());
    }

    #endregion

    #region Set

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (isEX) return;

        if (moveInput.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
            sp.flipX = false;
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            isFacingRight = false;
            sp.flipX = true;
        }
    }

    private void SetRigidbody(Vector2 newRb)
    {
        if (isEX)
        {
            rb.gravityScale = 0f;
            return;
        }

        rb.velocity = newRb;
        rb.gravityScale = 3f;
    }

    #endregion

    #region Return

    protected Vector3 MousePos()
    {
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));

        return point;
    }

    protected Vector3 MouseVec()
    {
        Vector3 vec = MousePos() - transform.position;
        return vec.normalized;
    }

    #endregion

    #region Coroutine

    protected virtual IEnumerator DashDelay()
    {
        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector2.zero;
        isDash = false;

        yield return new WaitForSeconds(dashDelay);

        pDash = true;
    }

    protected virtual IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);

        pAttack = true;
    }

    protected IEnumerator EXEvent()
    {
        isInvincibility = true;

        float setTime = 0.2f;
        float loopTime = 1.5f;

        yield return SlowTimeCor(setTime, loopTime, () => 
        {
            SpecialEffectSystem.Instance.BackgroundDarkness(loopTime * setTime);
        });

        isInvincibility = false;

        GameSystem.Instance.SetEX(true);
    }

    private IEnumerator DeathEvent()
    {
        isDead = true;

        float setTime = 0.1f;

        yield return SlowTimeCor(setTime, 1f, () => 
        {
            sp.DOColor(Color.red, 1f * setTime);
        });

        Death();
    }

    private IEnumerator SlowTimeCor(float slowTime, float loopTime, Action slowEvent)
    {
        Time.timeScale = slowTime;
        slowEvent?.Invoke();

        yield return new WaitForSeconds(slowTime * loopTime);

        Time.timeScale = 1f;
    }

        #endregion

}
