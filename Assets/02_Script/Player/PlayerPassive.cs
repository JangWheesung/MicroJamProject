using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PassiveType
{
    Passive_1, //����
    Passive_2, //����
    Passive_3, //����
    Passive_4, //�ս�
    Passive_5, //����ȭ
    Passive_6, //�ر�
    Passive_7, //�߰�
    Passive_8, //�й�
    Passive_9, //�ع�
    Passive_10, //����
    Passive_11, //����
    Passive_12, //���
    Passive_13, //�� �ð���
    Passive_14, //�J�� �ð���
    Passive_15, //�밡����
    Passive_16, //�밡����
    Passive_17, //����
    Passive_18, //�Ƿ�
    Passive_19, //�����
    Passive_20, //������
    Passive_21, //���
    Passive_22, //ȸ��
    Passive_23, //�ǰ���
    Passive_24, //���
    Passive_25, //�����ϻ�
    Passive_26, //����1
    Passive_27, //����2
    Passive_28, //����3
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
