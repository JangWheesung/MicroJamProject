using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PageData
{
    public int waveCnt;
    public float spawnTime;
    public float delayTime;
    private EnemyWaveData[] waveDatas;

    public void SetDatas(EnemyWaveData[] newData)
    {
        waveDatas = newData;
    }

    public EnemyWaveData[] GetDatas()
    {
        return waveDatas;
    }
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoint;

    [SerializeField] private List<PageData> pageDatas;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        //페이지모드
        int idx = 0;
        foreach (PageData data in pageDatas)
        {
            data.SetDatas(Resources.LoadAll<EnemyWaveData>($"WaveData/Page_{idx + 1}"));
            idx++;

            yield return PageLoop(data);
        }

        //무한모드
        PageData endless = pageDatas[pageDatas.Count - 1];
        endless.SetDatas(Resources.LoadAll<EnemyWaveData>("WaveData/Endless"));

        int extraCnt = 0;
        while (true)
        {
            extraCnt += Random.Range(1, 2);
            yield return PageLoop(endless, extraCnt);
        }
    }

    private IEnumerator PageLoop(PageData data, int extraCnt = 0)
    {
        for (int i = 0; i < data.waveCnt; i++)
        {
            int waveIdx = Random.Range(0, data.GetDatas().Length - 1);
            yield return WaveLoop(data.GetDatas()[waveIdx], data.spawnTime, extraCnt);
            yield return new WaitForSeconds(data.delayTime);
        }

        yield return null;
    }

    private IEnumerator WaveLoop(EnemyWaveData data, float spawnTime, int extraCnt = 0)
    {
        List<int> amountCnt = new List<int>();

        foreach (AmountData amountData in data.amoutDatas)
        {
            amountCnt.Add(amountData.cnt + extraCnt);
        }

        while (amountCnt.Count > 0)
        {
            int amountIdx = Random.Range(0, data.amoutDatas.Count - 1);
            AmountData amountData = data.amoutDatas[amountIdx];

            if (amountCnt[amountIdx] <= 0)
            {
                amountCnt.Remove(amountIdx);
                continue;
            }

            int randomPoint = Random.Range(0, spawnPoint.Length);
            PoolingManager.Instance.Pop<IEnemy>(amountData.enemy.name, spawnPoint[randomPoint].position);
            yield return new WaitForSeconds(spawnTime);

            amountCnt[amountIdx]--;
        }

        yield return null;
    }
}
