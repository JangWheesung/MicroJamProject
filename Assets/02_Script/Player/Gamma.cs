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

        AttackEffeectBase effect = PoolingManager.instance.Pop<AttackEffeectBase>(attackEffect.name, transform.position);
        effect.PopEffect(MouseVec());

        //AudioManager.Instance.StartSfx($"Smashing{idx}");
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

    protected override IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);

        pAttack = true;
        currentBullet = bulletCount;
    }
}
