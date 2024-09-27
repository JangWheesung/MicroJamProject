using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PassiveType
{
    Passive_1, //과속
    Passive_2, //저속
    Passive_3, //축적
    Passive_4, //손실
    Passive_5, //파편화
    Passive_6, //붕괴
    Passive_7, //견고
    Passive_8, //압박
    Passive_9, //해방
    Passive_10, //구속
    Passive_11, //도약
    Passive_12, //비상
    Passive_13, //긴 시간선
    Passive_14, //짫은 시간선
    Passive_15, //대가증폭
    Passive_16, //대가부재
    Passive_17, //폭주
    Passive_18, //피로
    Passive_19, //무방비
    Passive_20, //재정비
    Passive_21, //대비
    Passive_22, //회귀
    Passive_23, //되감기
    Passive_24, //상실
    Passive_25, //구사일생
    Passive_26, //기적1
    Passive_27, //기적2
    Passive_28, //기적3
}

public class PlayerPassive : MonoBehaviour
{
    public void SetPassive()
    {
        var player = GetComponent<PlayerBase>();
        
        foreach (var passive in player.passiveTypes)
        {
            var dataList = Resources.LoadAll<PassiveData>("PassiveDatas").ToList();
            var findData = dataList.Find(x => x.passiveType == passive);
            findData.AddPassive(player);
        }
    }
}
