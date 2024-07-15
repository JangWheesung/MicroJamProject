using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttackEffect : AttackEffectBase
{
    [SerializeField] protected ParticleSystem bulletParticle;
    [SerializeField] protected float bulletRadius;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected bool bulletPenetrate;
    protected Rigidbody2D rb;

    public override void PopEffect(Vector2 vec)
    {
        //파티클 방향
        var mainModule = bulletParticle.main;
        float angle = Mathf.Atan2(vec.y, -vec.x);

        mainModule.startRotation = angle;
        bulletParticle.Play();

        //총알 방향
        transform.right = -vec;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = vec * bulletSpeed;
    }

    protected void Update()
    {
        HitRader(transform.position, bulletRadius);
    }

    protected override void PlayerHit(PlayerBase player)
    {
        base.PlayerHit(player);

        if(!bulletPenetrate)
            DisableEffect();
    }

    protected override void EnemyHit(IEnemy enemy, Transform enemyTrs)
    {
        base.EnemyHit(enemy, enemyTrs);

        if (!bulletPenetrate)
            DisableEffect();
    }

    public override void DisableEffect()
    {
        rb.velocity = Vector2.zero;
        base.DisableEffect();
    }
}
