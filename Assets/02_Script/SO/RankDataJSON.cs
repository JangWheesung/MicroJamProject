using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RankDataJSON
{
    public string characterSpriteName; // Sprite의 이름을 저장 (경로 대신 Resources에서 로드할 수 있도록)

    public string characterName;
    public string nickName;
    public float killCount;
    public float timeCount;

    public RankDataJSON() { }

    // 초기화 함수
    public RankDataJSON(Sprite characterSprite, string characterName, string nickName, float killCount, float timeCount)
    {
        // Sprite는 이름만 저장하고 Resources 폴더에서 나중에 로드
        this.characterSpriteName = characterSprite.name;
        this.characterName = characterName;
        this.nickName = nickName;
        this.killCount = killCount;
        this.timeCount = timeCount;
    }
}
