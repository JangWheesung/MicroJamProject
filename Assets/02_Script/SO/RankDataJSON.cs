using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RankDataJSON
{
    public string characterSpriteName; // Sprite�� �̸��� ���� (��� ��� Resources���� �ε��� �� �ֵ���)

    public string characterName;
    public string nickName;
    public float killCount;
    public float timeCount;

    public RankDataJSON() { }

    // �ʱ�ȭ �Լ�
    public RankDataJSON(Sprite characterSprite, string characterName, string nickName, float killCount, float timeCount)
    {
        // Sprite�� �̸��� �����ϰ� Resources �������� ���߿� �ε�
        this.characterSpriteName = characterSprite.name;
        this.characterName = characterName;
        this.nickName = nickName;
        this.killCount = killCount;
        this.timeCount = timeCount;
    }
}
