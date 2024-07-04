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

        while (!ControlSystem.Instance.isDeath)
        {
            Debug.Log(waveCnt);
            enemyCnt += Random.Range(2, 4);
            spawnTime = waveCnt switch
            {
                < 5 => 0.5f,
                < 10 => 0.4f,
                < 15 => 0.3f,
                _ => 0.2f
            };

            float delayTime = waveCnt switch
            {
                < 5 => 5f,
                < 10 => 4f,
                < 15 => 3f,
                < 20 => 2f,
                < 25 => 1f,
                < 30 => 0.5f,
                _ => 0.4f
            };

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
