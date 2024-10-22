using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Gamma : PlayerBase
{
    [Header("GammaBase")]
    [SerializeField] private BoostEffect skillEffect;
    [SerializeField] private TMP_Text bulletText;
    [SerializeField] private int bulletCount;
    [SerializeField] protected float dashSpeed = 15f;
    [SerializeField] protected float dashDuration = 0.2f;

    private int currentBullet;
    private int upgradeBullet;
    private bool isDash;

    private bool isEmpty => currentBullet <= 0;
    private bool isUpgrade => upgradeBullet > 0;

    protected override void Start()
    {
        base.Start();

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

        AudioManager.Instance.StartSfx($"Dash");

        Vector2 dashVelocity = new Vector2(isFacingRight ? dashSpeed : -dashSpeed, rb.velocity.y);
        SetRigidbody(dashVelocity);

        StartCoroutine(DashDelay());

        int spinDown = isFacingRight ? -180 : 180;
        int spinUp = isFacingRight ? -360 : 360;

        isInvincibility = true;
        sp.color = Color.gray;

        DOTween.Sequence()
            //.Append(transform.DORotate(new Vector3(0, 0, spinDown), dashDuration / 2f))
            //.Append(transform.DORotate(new Vector3(0, 0, spinUp), dashDuration / 2f))
            .AppendInterval(dashDuration)
            .OnComplete(() =>
            {
                SetRigidbody(Vector2.zero);
                StartCoroutine(SkillAttackDelay());

                isInvincibility = false;
                sp.color = Color.white;
                transform.rotation = new Quaternion(0, 0, 0, 0);
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
        if (!pAttack) return;

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

    private IEnumerator SkillAttackDelay()
    {
        int deg = 30;
        float spinDeg = 360;
        float delayTime = dashDuration / (spinDeg / deg);

        for (int i = 0; i < spinDeg; i += deg)
        {
            float radian = i * Mathf.Deg2Rad;
            Vector2 fireVec = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

            GammaAttackEffect effect = PoolingManager.Instance.Pop<GammaAttackEffect>(attackEffect.name, transform.position);
            effect.SetTimeAmount(attackValue);
            effect.PopEffect(fireVec, isUpgrade);

            AudioManager.Instance.StartSfx("Bullet_1", 0.6f);
            SpecialEffectSystem.Instance.CameraShaking(CameraType.Shake_S);
            SpecialEffectSystem.Instance.CameraShaking(CameraType.Rock_S, delayTime);

            yield return new WaitForSeconds(delayTime);
        }
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
