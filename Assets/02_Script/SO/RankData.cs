using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Rank", fileName = "NewRankData")]
public class RankData : ScriptableObject
{
    public Sprite characterSprite;
    public string characterName;
    public string nickName;
    public float killCount;
    public float timeCount;


    public void Initialize(Sprite characterSprite, string characterName, string nickName, float killCount, float timeCount)
    {
        this.characterSprite = characterSprite;
        this.characterName = characterName;
        this.nickName = nickName;
        this.killCount = killCount;
        this.timeCount = timeCount;
    }
}
