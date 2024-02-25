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

        while (true)
        {
            enemyCnt += Random.Range(2, 5);
            spawnTime = waveCnt switch
            {
                < 10 => 0.5f,
                < 20 => 0.4f,
                < 30 => 0.3f
            };

            Debug.Log(spawnTime);

            float delayTime = Random.Range(3f, 5f);
            yield return new WaitForSeconds(delayTime);

            for (int i = 0; i < enemyCnt; i++)
            {
                int randomPoint = Random.Range(0, spawnPoint.Length);
                PoolingManager.instance.Pop<Enemy>(enemy.name, spawnPoint[randomPoint].position);

                yield return new WaitForSeconds(spawnTime);
            }

            waveCnt++;
        }
    }
}
