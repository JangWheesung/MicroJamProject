using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum AbilityStat
{
    None,
    Jump,
    Dash,
    Attack,
    EX
};

[System.Serializable]
public struct PassiveData
{
    public PassiveType type;
    public string name;
    public string ext;
}

[CreateAssetMenu(fileName = "???Stat", menuName = "SO/Character/Stat")]
public class CharacterStat : ScriptableObject
{
    [Header("Base")]
    public GameObject player;
    public Sprite characterSprite;
    public Color characterColor;
    public string chatacterName;

    [Header("Value")]
    public float speedStat;
    public float jumpStat;
    public float attackStat;
    public float skillDelayStat;
    public float attackDelayStat;

    [Header("Attack")]
    public string attackExt;
    public string exName;
    public string exExt;

    [Header("Passive")]
    public PassiveData[] passiveDatas;
    public PassiveType[] GetPassiveTypes()
    {
        return passiveDatas.Select(p => p.type).ToArray();
    }
}
