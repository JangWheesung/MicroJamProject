using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PassiveType
{
    Passive_1,
    Passive_2,
    Passive_3,
    Passive_4,
    Passive_5,
    Passive_6,
    Passive_7,
    Passive_8,
    Passive_9,
    Passive_10,
    Passive_11,
    Passive_12,
    Passive_13,
    Passive_14,
    Passive_15,
    Passive_16,
    Passive_17,
    Passive_18,
    Passive_19,
    Passive_20,
    Passive_21,
    Passive_22,
    Passive_23,
    Passive_24,
    Passive_25,
    Passive_26,
    Passive_27,
    Passive_28,
    Passive_29,
    Passive_30,
}

public class PlayerPassive : MonoBehaviour
{
    public void SetPassive()
    {
        var player = GetComponent<PlayerBase>();
        
        foreach (var passive in player.passiveDatas)
        {
            var dataList = Resources.LoadAll<PassiveBase>("PassiveData").ToList();
            var findData = dataList.Find(x => x.passiveType == passive);
            findData.AddPassive(player);
        }
    }
}
