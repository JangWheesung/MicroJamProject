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
    public float speedStat;
    public float jumpStat;
    public float attackStat;

    [Header("Attack")]
    public string attackExt;
    public string exName;
    public string exExt;
    public float attackDelayStat;

    [Header("Skill")]
    public string skillName;
    public string skillExt;
    public float skillDelayStat;


    [Header("Passive")]
    public PassiveData[] passiveDatas;

    public PassiveType[] GetPassiveTypes()
    {
        return passiveDatas.Select(p => p.passiveType).ToArray();
    }
}
