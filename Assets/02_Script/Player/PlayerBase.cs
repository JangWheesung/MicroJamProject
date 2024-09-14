using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerBase : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] protected AttackEffectBase attackEffect;
    [SerializeField] protected EXEffectBase exEffect;
    [SerializeField] protected Slice playerSlice;

    [Header("Data_Base")]
    [HideInInspector] public Color playerColor;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float jumpPower;
    [HideInInspector] public float skillDelay;
    [HideInInspector] public float attackDelay;

    [Header("Data_EX")]
    [HideInInspector] public int jumpCount;
    [HideInInspector] public float originGravity;

    protected bool isFacingRight = true;
    protected bool isGrounded = false;
    protected bool isSlow = false;
    protected bool isEX;
    protected bool isDead = false;

    protected bool pSkill = true;
    protected bool pAttack = true;

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
        Vector2 moveVec = new Vector2(MovementVector.x * moveSpeed, rb.velocity.y);
        SetRigidbody(moveVec);
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

    public void OnSkill(InputAction.CallbackContext context)
    {
        if (isDead) return;
        
        if (context.started)
        {
            Skill();
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

    protected virtual void Skill()
    {
        if (!pSkill) return;

        pSkill = false;
        StartCoroutine(SkillDelay());
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
        SetRigidbody(new Vector2(0f, 3f));
    }

    private void HandleDeath()
    {
        StartCoroutine(DeathEvent());
    }

    #endregion

    #region Set

    public void SetDataBase(Color color, float speed, float jump, float skill, float attack)
    {
        playerColor = color;
        moveSpeed = speed;
        jumpPower = jump;
        skillDelay = skill;
        attackDelay = attack;
    }

    protected void SetFacingDirection(Vector2 moveInput)
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

    protected void SetRigidbody(Vector2 newRb)
    {
        if (isEX)
        {
            rb.gravityScale = 0f;
            return;
        }

        rb.velocity = newRb;
        rb.gravityScale = originGravity;
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

    protected virtual IEnumerator SkillDelay()
    {
        yield return new WaitForSeconds(skillDelay);

        pSkill = true;
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
