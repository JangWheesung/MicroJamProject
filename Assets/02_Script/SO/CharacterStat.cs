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

[CreateAssetMenu(fileName = "???Stat", menuName = "SO/Character/Stat")]
public class CharacterStat : ScriptableObject
{
    [Header("Base")]
    public GameObject player;
    public Sprite characterSprite;
    public Color characterColor;
    public string chatacterName;

    [Header("Value")]
    [Range(0, 12f)] public float speedStat;
    [Range(0, 15f)] public float jumpStat;
    [Range(0, 2f)] public float attackStat;

    [Space(20f)]
    [Header("Attack")]
    public string attackExt;

    [Space(5f)]
    public string exName;
    [TextArea] public string exExt;
    [Range(0, 1f)] public float attackDelayStat;

    [Space(20f)]
    [Header("Skill")]
    public string skillName;
    [TextArea] public string skillExt;
    [Range(0, 10f)] public float skillDelayStat;

    [Space(20f)]
    [Header("Passive")]
    public PassiveData[] passiveDatas;

    public PassiveType[] GetPassiveTypes()
    {
        return passiveDatas.Select(p => p.passiveType).ToArray();
    }
}
