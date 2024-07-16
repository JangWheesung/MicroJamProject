using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpeedStat
{
    Slow,
    Normal,
    Fast
};

public enum AbilityStat
{
    None,
    Jump,
    Dash,
    Attack,
    EX
};

[System.Serializable]
public struct AbilityData
{
    public AbilityStat abilityStat;
    public string name;
    public string ext;
}

[CreateAssetMenu(fileName = "???Stat", menuName = "SO/Character/Stat")]
public class CharacterStat : ScriptableObject
{
    public GameObject player;
    public Sprite characterSprite;
    public Color characterColor;
    public string chatacterName;

    public SpeedStat speedStat;
    public int jumpStat;
    public float dashStat;

    public string attackExt;
    public string exName;
    public string exExt;

    public AbilityData[] abilityDatas;
}
