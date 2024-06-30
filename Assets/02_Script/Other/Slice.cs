using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour
{
    [SerializeField] private float breakTime;
    private Rigidbody2D[] slicesRigbody;

    private void Awake()
    {
        slicesRigbody = GetComponentsInChildren<Rigidbody2D>();
    }

    public void BreakEffect(float breakPower = 5)
    {
        foreach (Rigidbody2D sliceRb in slicesRigbody)
        {
            sliceRb.transform.position = transform.position;

            float x = Random.Range(-1f, 1f);
            float y = 1 - Mathf.Pow(Mathf.Abs(x), 2);

            sliceRb.AddForce(new Vector2(x, y) * breakPower, ForceMode2D.Impulse);
        }

        StartCoroutine(PopSlice());
    }

    private IEnumerator PopSlice()
    {
        yield return new WaitForSeconds(breakTime);

        foreach (Rigidbody2D sliceRb in slicesRigbody)
        {
            sliceRb.velocity = Vector2.zero;
            sliceRb.transform.position = transform.position;
        }

        PoolingManager.Instance.Push(gameObject);
    }
}
