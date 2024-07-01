using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergeSpawner : MonoBehaviour
{
    [SerializeField] private EXGaugeBar gaugeBar;
    [SerializeField] private Energe energeObj;
    [SerializeField] private float posX, posY;
    [SerializeField] private float length, height;
    [SerializeField] private float spawnCool;

    private void Start()
    {
        StartCoroutine(EnergeSpawnerCor());
    }

    private float RandomRange(float value)
    {
        return Random.Range(-value, value);
    }

    private IEnumerator EnergeSpawnerCor()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnCool + RandomRange(10f));

            Vector2 spawnPos = new Vector2(posX + RandomRange(length / 2), posY + RandomRange(-(height / 2)));
            Energe energe = PoolingManager.Instance.Pop<Energe>(energeObj.name, spawnPos);
            energe.PopEnerge(gaugeBar);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Vector2 center = new Vector2(posX, posY);
        Vector2 size = new Vector2(length, height);
        Gizmos.DrawWireCube(center, size);
    }
#endif
}
