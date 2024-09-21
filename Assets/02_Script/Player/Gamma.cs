using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gamma : PlayerBase
{
    [Header("GammaBase")]
    [SerializeField] private TMP_Text bulletText;
    [SerializeField] private int bulletCount;
    private int currentBullet;
    private int upgradeBullet;

    private bool isEmpty => currentBullet <= 0;
    private bool isUpgrade => upgradeBullet > 0;

    protected override void Start()
    {
        base.Start();

        currentBullet = bulletCount;
        BulletCountCheck();
    }

    protected override void Skill() //¾ÆÁ÷ Ã¶°©Åº º¸·ù
    {
        if (!pSkill) return;

        if(currentBullet >= 3)
            upgradeBullet = 3;
        else
            upgradeBullet = currentBullet;

        BulletCountCheck();

        AudioManager.Instance.StartSfx("EnergeUp", 0.6f);

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

    protected override IEnumerator AttackDelay()
    {
        AudioManager.Instance.StartSfx("Reload");

        yield return new WaitForSeconds(attackDelay);

        pAttack = true;
        currentBullet = bulletCount;

        BulletCountCheck();
    }
}
