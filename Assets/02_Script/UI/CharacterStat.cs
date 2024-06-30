using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpeedStat
{
    Slow,
    Normal,
    Fast
};

[System.Serializable]
public struct AbilityStat
{
    public string name;
    public string ext;
}

[CreateAssetMenu(fileName = "???Stat", menuName = "SO/Character/Stat")]
public class CharacterStat : ScriptableObject
{
    public GameObject player;
    public Sprite characterSprite;
    public SpeedStat speedStat;
    public Color characterColor;
    public string chatacterName;
    public int jumpStat;
    public float dashStat;
    public string attackExt;
    public AbilityStat[] abilityStats;
}
