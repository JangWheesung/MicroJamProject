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
    }

    protected override void Attack()
    {
        if (!pAttack) return;

        currentBullet--;
        BulletCountCheck();

        AttackEffeectBase effect = PoolingManager.Instance.Pop<AttackEffeectBase>(attackEffect.name, transform.position);
        effect.PopEffect(MouseVec());

        AudioManager.Instance.StartSfx("Bullet");
        CinemachineEffectSystem.Instance.CinemachineShaking();
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
        bulletText.fontSize = isEmpty ? 1.2f : 1.8f;
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
