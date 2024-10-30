using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Gamma : PlayerBase
{
    [Header("GammaBase")]
    [SerializeField] private BoostEffect skillEffect_reload;
    [SerializeField] private BoostEffect skillEffect_set;
    [SerializeField] private AttackEffectBase skillEffect_attack;
    [SerializeField] private TMP_Text bulletText;
    [SerializeField] private int bulletCount;
    [SerializeField] private float bulletDelay;
    [SerializeField] protected float dashSpeed = 15f;
    [SerializeField] protected float dashDuration = 0.2f;

    private int currentBullet;
    private int upgradeBullet;
    private bool isDash;

    private bool pFire;

    private bool isEmpty => currentBullet <= 0;
    private bool isUpgrade => upgradeBullet > 0;

    protected override void Start()
    {
        base.Start();

        pFire = true;
        currentBullet = bulletCount;
        BulletCountCheck();
    }

    protected override void FixedUpdate()
    {
        if (isDash) return;

        base.FixedUpdate();
    }

    protected override void Skill()
    {
        if (!pSkill) return;

        isDash = true;

        StartCoroutine(DashDelay());

        AudioManager.Instance.StartSfx($"Dash");
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_H);

        var skillSetEffect = PoolingManager.Instance.Pop<BoostEffect>(skillEffect_set.name, transform.position);
        skillSetEffect.PopEffect(this);

        Vector2 dashVelocity = new Vector2(isFacingRight ? dashSpeed : -dashSpeed, rb.velocity.y);
        SetRigidbody(dashVelocity);

        isInvincibility = true;
        SetSpriteColor(Color.gray);

        int spinDown = isFacingRight ? -180 : 180;
        int spinUp = isFacingRight ? -360 : 360;

        DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0, 0, spinDown), dashDuration / 2f))
            .Append(transform.DORotate(new Vector3(0, 0, spinUp), dashDuration / 2f))
            //.AppendInterval(dashDuration)
            .AppendCallback(() =>
            {
                SetRigidbody(Vector2.zero);

                var skillAttackEffect = PoolingManager.Instance.Pop<AttackEffectBase>(skillEffect_attack.name, transform.position);
                skillAttackEffect.SetTimeAmount(attackAmount);
                skillAttackEffect.PopEffect(this, true);

                currentBullet = 0;
                BulletCountCheck();

                isInvincibility = false;
                SetSpriteColor(Color.white);
                transform.rotation = new Quaternion(0, 0, 0, 0);

                SpecialEffectSystem.Instance.CameraShaking(CameraType.Rock_H, 0.6f);
                AudioManager.Instance.StartSfx($"GammaSkill", 1.2f);
            });

        //if (currentBullet >= 3)
        //    upgradeBullet = 3;
        //else
        //    upgradeBullet = currentBullet;

        //bulletText.transform.DOShakeScale(0.2f).SetEase(Ease.OutBounce);
        //BulletCountCheck();

        //BoostEffect effect = PoolingManager.Instance.Pop<BoostEffect>(skillEffect.name, transform.position);
        //effect.PopEffect(this);

        //AudioManager.Instance.StartSfx("EnergeDown", 0.8f);

        base.Skill();
    }

    protected override void Attack()
    {
        if (!pFire) return;
        if (!pAttack) return;

        pFire = false;
        StartCoroutine(BulletDelay());

        GammaAttackEffect effect = PoolingManager.Instance.Pop<GammaAttackEffect>(attackEffect.name, transform.position);
        effect.SetTimeAmount(attackValue);
        effect.PopEffect(MouseVec(), isUpgrade);

        currentBullet--;
        upgradeBullet--;
        BulletCountCheck();

        AudioManager.Instance.StartSfx("Bullet_1", 0.6f);
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Rock_S);
    }

    private void BulletCountCheck()
    {
        if (isEmpty)
        {
            pAttack = false;
            StartCoroutine(AttackDelay());

            var skillReloadEffect = PoolingManager.Instance.Pop<BoostEffect>(skillEffect_reload.name, transform.position);
            skillReloadEffect.PopEffect(this);
        }
        bulletText.color = isUpgrade ? Color.yellow : Color.white;
        bulletText.text = isEmpty ? "Reloading..." : $"{currentBullet}/{bulletCount}";
        bulletText.fontSize = isEmpty ? 1.7f : 2.4f;
    }

    protected override void EX()
    {
        rb.velocity = Vector2.zero;

        EXEffectBase effect = PoolingManager.Instance.Pop<EXEffectBase>(exEffect.name, Vector2.zero);
        effect.SetTimeAmount(exValue);
        effect.PopEffect(this);
    }

    protected override void Death()
    {
        bulletText.gameObject.SetActive(false);

        base.Death();
    }

    private IEnumerator DashDelay()
    {
        yield return new WaitForSeconds(dashDuration);

        isDash = false;
        SetRigidbody(Vector2.zero);
    }

    protected IEnumerator BulletDelay()
    {
        yield return new WaitForSeconds(bulletDelay);

        pFire = true;
    }

    protected override IEnumerator AttackDelay()
    {
        AudioManager.Instance.StartSfx("Reload");

        yield return new WaitForSeconds(attackDelay);

        pAttack = true;
        currentBullet = bulletCount;

        BulletCountCheck();
    }
}
