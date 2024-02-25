using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashingEffect : MonoBehaviour
{
    private Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Smashing", true);

        float rotateZ = Random.Range(-30f, 30f);
        transform.Rotate(new Vector3(0, 0, rotateZ));

        StartCoroutine(PopEffect());
    }

    IEnumerator PopEffect()
    {
        yield return new WaitForSeconds(1f);

        animator.SetBool("Smashing", false);
        PoolingManager.instance.Push(gameObject);
    }
}
