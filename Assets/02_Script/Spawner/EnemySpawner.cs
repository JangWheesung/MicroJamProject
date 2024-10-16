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

    private int waveCount;
    private int extraCnt;
    private float upgradeAmount;

    private bool isEndless;

    private void Start()
    {
        ControlSystem.Instance.OnGameStartEvt += StartEnemySpawn;
        ControlSystem.Instance.OnDeathEvt += EnmeyDestory;
    }

    private void StartEnemySpawn()
        => StartCoroutine(SpawnLoop());

    private void EnmeyDestory()
    {
        foreach (GameObject enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemyObj.TryGetComponent(out IEnemy enemy))
            {
                enemy.Death(0f);
            }
        }
    }

    private void WaveTriggerEvent()
    {
        if (ControlSystem.Instance.IsStopLogic()) return;

        waveCount++;

        string waveReading;
        if (!isEndless)
        {
            waveReading = "새로운 적 출현!";
        }
        else
        {
            int randomEvent = Random.Range(0, 2);
            if (randomEvent == 0 && upgradeAmount < 1f)
            {
                waveReading = "적 변종 츌현율 증가!";
                upgradeAmount += 0.1f;
            }
            else
            {
                waveReading = "적 생성횟수 증가!";
                extraCnt += Random.Range(1, 2);
            }
        }

        UISystem.Instance.WaveUI.PopWaveSetting(waveCount, waveReading);
    }

    private IEnumerator SpawnLoop()
    {
        //페이지모드
        int idx = 0;
        foreach (PageData data in pageDatas)
        {
            WaveTriggerEvent();

            data.SetDatas(Resources.LoadAll<EnemyWaveData>($"WaveData/Page_{idx + 1}"));
            idx++;

            yield return PageLoop(data);
        }

        //무한모드
        isEndless = true;
        PageData endless = pageDatas[pageDatas.Count - 1];
        endless.SetDatas(Resources.LoadAll<EnemyWaveData>("WaveData/Endless"));
        while (true)
        {
            WaveTriggerEvent();

            yield return PageLoop(endless);
        }
    }

    private IEnumerator PageLoop(PageData data)
    {
        for (int i = 0; i < data.waveCnt; i++)
        {
            int waveIdx = Random.Range(0, data.GetDatas().Length - 1);
            yield return WaveLoop(data.GetDatas()[waveIdx], data.spawnTime);
            yield return new WaitForSeconds(data.delayTime);
        }

        yield return null;
    }

    private IEnumerator WaveLoop(EnemyWaveData data, float spawnTime)
    {
        List<int> amountCnt = new List<int>();

        foreach (AmountData amountData in data.amoutDatas)
        {
            amountCnt.Add(amountData.cnt + extraCnt);
        }

        while (amountCnt.Count > 0)
        {
            int amountIdx = Random.Range(0, amountCnt.Count);
            AmountData amountData = data.amoutDatas[amountIdx];

            if (amountCnt[amountIdx] <= 0)
            {
                amountCnt.RemoveAt(amountIdx);
                continue;
            }

            int randomPoint = Random.Range(0, spawnPoint.Length);
            bool randomUpgrade = Random.Range(0, 1f) < upgradeAmount;
            IEnemy enemy = PoolingManager.Instance.Pop<IEnemy>(amountData.enemy.name, spawnPoint[randomPoint].position);
            if (randomUpgrade)
                enemy.Upgrade();
           

            yield return new WaitForSeconds(spawnTime);

            amountCnt[amountIdx]--;
        }

        yield return null;
    }
}
