using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoint;

    [SerializeField] private Enemy enemy;

    private void Start()
    {
        StartCoroutine(WaveLoop());
    }

    private IEnumerator WaveLoop()
    {
        int waveCnt = 0;
        int enemyCnt = 2;
        float spawnTime = 0.5f;

        while (!GameoverSystem.Instance.isDeath)
        {
            enemyCnt += Random.Range(2, 5);
            spawnTime = waveCnt switch
            {
                < 8 => 0.5f,
                < 16 => 0.4f,
                < 24 => 0.3f,
                < 30 => 0.2f
            };

            float delayTime = Random.Range(4f, 5f);
            if (waveCnt == 0) delayTime = 1f;
            yield return new WaitForSeconds(delayTime);

            for (int i = 0; i < enemyCnt; i++)
            {
                int randomPoint = Random.Range(0, spawnPoint.Length);
                PoolingManager.Instance.Pop<Enemy>(enemy.name, spawnPoint[randomPoint].position);

                yield return new WaitForSeconds(spawnTime);
            }

            waveCnt++;
        }
    }
}