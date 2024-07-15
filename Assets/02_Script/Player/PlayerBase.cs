using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerBase : MonoBehaviour
{
    [Header("Base_Prefab")]
    [SerializeField] protected AttackEffectBase attackEffect;
    [SerializeField] protected EXEffectBase exEffect;
    [SerializeField] protected Slice playerSlice;

    [Header("Base_Value")]
    [SerializeField] protected Color playerColor;
    [SerializeField] protected float moveSpeed = 9f;
    [SerializeField] protected float jumpPower = 12f;
    [SerializeField] protected int jumpCount = 2;
    [SerializeField] protected float dashSpeed = 23f;
    [SerializeField] protected float dashDuration = 0.2f;
    [SerializeField] protected float dashDelay = 1f;
    [SerializeField] protected float attackDelay = 0.25f;

    protected bool isFacingRight = true;
    protected bool isGrounded = false;
    protected bool isDash = false;
    protected bool isSlow = false;
    protected bool isEX;
    protected bool isDead = false;

    protected bool pAttack = true;
    protected bool pDash = true;

    protected int currentJumpCount = 0;
    protected bool isInvincibility = false;

    protected Rigidbody2D rb;
    protected SpriteRenderer sp;
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
        ControlSystem.Instance.OnExStartEvt += HandleStartEX;
        ControlSystem.Instance.OnExEndEvt += HandleEndEX;
        ControlSystem.Instance.OnDeathEvt += HandleDeath;

        UISystem.Instance.Profle.ProfleSetting(playerColor, sp.sprite);
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
        if (isDead || isEX) return;

        if (context.started)
        {
            StartCoroutine(EXEvent());
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

        AttackEffectBase effect = PoolingManager.Instance.Pop<AttackEffectBase>(attackEffect.name, transform.position);
        effect.PopEffect();

        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_S);
    }

    protected virtual void EX()
    {
        rb.velocity = Vector2.zero;

        EXEffectBase effect = PoolingManager.Instance.Pop<EXEffectBase>(exEffect.name, transform.position);
        effect.PopEffect();
    }

    #endregion

    #region Virtual_Event

    public virtual void Hit(float minusTime)
    {
        if (isInvincibility) return;

        TimeSystem.Instance.MinusTime(minusTime);
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

    private void HandleStartEX()
    {
        EX();
    }

    private void HandleEndEX()
    {
        isEX = false;
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
        if (!UISystem.Instance.EXGaugeBar.IsCharging) yield break;

        isInvincibility = true;
        isEX = true;

        float setTime = 0.2f;
        float loopTime = 1.5f;

        yield return SlowTimeCor(setTime, loopTime, (delayTime) => 
        {
            UISystem.Instance.Profle.PopProfle(delayTime);
            SpecialEffectSystem.Instance.BackgroundDarkness(delayTime);
            AudioManager.Instance.StartSfx("TimeSlow");
        });

        isInvincibility = false;
        ControlSystem.Instance.SetEX(true);
    }

    private IEnumerator DeathEvent()
    {
        isDead = true;

        float setTime = 0.1f;

        yield return SlowTimeCor(setTime, 1f, (delayTime) => 
        {
            sp.DOColor(Color.red, delayTime);
        });

        Death();
    }

    private IEnumerator SlowTimeCor(float slowTime, float loopTime, Action<float> slowEvent)
    {
        Time.timeScale = slowTime;

        float delayTime = slowTime * loopTime;
        slowEvent?.Invoke(delayTime);
        yield return new WaitForSeconds(delayTime);

        Time.timeScale = 1f;
    }

        #endregion

}
