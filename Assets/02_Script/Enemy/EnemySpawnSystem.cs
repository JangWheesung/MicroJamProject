using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
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
            spawnTime = waveCnt switch
            {
                < 8 => 0.5f,
                < 16 => 0.4f,
                < 24 => 0.3f,
                < 30 => 0.2f
            };

            float delayTime = Random.Range(4f, 5f);
            yield return new WaitForSeconds(delayTime);

            for (int i = 0; i < enemyCnt; i++)
            {
                int randomPoint = Random.Range(0, spawnPoint.Length);
                PoolingManager.instance.Pop<Enemy>(enemy.name, spawnPoint[randomPoint].position);

                yield return new WaitForSeconds(spawnTime);
            }

            enemyCnt += Random.Range(2, 5);
            waveCnt++;
        }
    }
}
