using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmashingEffect : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    private Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Smashing", true);

        EnemyHit();

        StartCoroutine(PopEffect());
    }

    private void EnemyHit()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Death();
                GameoverSystem.Instance.GetKillCount();

                int score = Random.Range(1, 3); //1~2
                TimeSystem.Instance.PlusTime(score);
            }
        }
    }

    IEnumerator PopEffect()
    {
        yield return new WaitForSeconds(0.3f);

        animator.SetBool("Smashing", false);
        PoolingManager.instance.Push(gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
#endif
}
