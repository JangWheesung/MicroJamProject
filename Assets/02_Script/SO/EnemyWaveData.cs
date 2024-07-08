using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AmountData
{
    public GameObject enemy;
    public int cnt;
}

[CreateAssetMenu(fileName = "WaveData", menuName = "SO/EnemyWave/Data")]
public class EnemyWaveData : ScriptableObject
{
    public List<AmountData> amoutDatas;
}
