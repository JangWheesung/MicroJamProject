using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum HandlePlayerBaseType
{
    AddStart,
    AddHit,
    AddAttack,
    AddStartEx,
    AddEndEx,
    AddDeath,
};

[CreateAssetMenu(fileName = "Passive_X", menuName = "SO/Character/Passive")]
public class PassiveData : ScriptableObject
{
    public PassiveType passiveType;
    public HandlePlayerBaseType handlePlayerBaseType;
    public string passiveName;
    public string passiveExt;

    private Action<PlayerBase> passiveAction;

    public void AddPassive(PlayerBase player) //이벤트애 HandlePassive를 구독
    {
        Action action = () => HandlePassive(player);

        switch (handlePlayerBaseType)
        {
            case HandlePlayerBaseType.AddStart:
                player.OnSettingPlayerEvt += action;
                break;
            case HandlePlayerBaseType.AddHit:
                player.OnHitPlayerEvt += action;
                break;
            case HandlePlayerBaseType.AddAttack:
                player.OnAttackPlayerEvt += action;
                break;
            case HandlePlayerBaseType.AddStartEx:
                player.OnStartExPlayerEvt += action;
                break;
            case HandlePlayerBaseType.AddEndEx:
                player.OnEndExPlayerEvt += action;
                break;
            case HandlePlayerBaseType.AddDeath:
                player.OnDeathPlayerEvt += action;
                break;
            default:
                Debug.LogError("Handle is null for Passive");
                break;
        }
    }

    private void HandlePassive(PlayerBase player)
    {
        passiveAction?.Invoke(player);
    }

    public void SetPassiveAction(Action<PlayerBase> action)
    {
        passiveAction = action;
    }
}
