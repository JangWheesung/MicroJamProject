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

    protected override void Start()
    {
        base.Start();

        currentBullet = bulletCount;
        BulletCountCheck();
    }

    protected override void Skill() //¾ÆÁ÷ Ã¶°©Åº º¸·ù
    {
        base.Skill();
    }

    protected override void Attack()
    {
        if (!pAttack) return;

        currentBullet--;
        BulletCountCheck();

        AttackEffectBase effect = PoolingManager.Instance.Pop<AttackEffectBase>(attackEffect.name, transform.position);
        effect.SetTimeAmount(attackValue);
        effect.PopEffect(MouseVec());

        AudioManager.Instance.StartSfx("Bullet_1", 0.6f);
        SpecialEffectSystem.Instance.CameraShaking(CameraType.Rock_S);
    }

    private void BulletCountCheck()
    {
        bool isEmpty = currentBullet <= 0;

        if (isEmpty)
        {
            pAttack = false;
            StartCoroutine(AttackDelay());
        }
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
