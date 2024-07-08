using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttackEffect : AttackEffeectBase
{
    [SerializeField] private ParticleSystem bulletParticle;
    [SerializeField] private float bulletRadius;
    [SerializeField] private float bulletSpeed;
    private Rigidbody2D rb;

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

    private void Update()
    {
        HitRader(transform.position, bulletRadius);
    }

    protected override void EnemyHit(IEnemy enemy, Transform enemyTrs)
    {
        base.EnemyHit(enemy, enemyTrs);

        int score = Random.Range(1, 3); //1 ~ 2
        TimeSystem.Instance.PlusTime(score);
    }

    public override void DisableEffect()
    {
        rb.velocity = Vector2.zero;
        //Time.timeScale = 0;
        base.DisableEffect();
    }
}
