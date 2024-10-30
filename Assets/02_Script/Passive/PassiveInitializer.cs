using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveInitializer : MonoBehaviour
{
    private Dictionary<PassiveType, Action<PlayerBase>> passiveActions;

    public void SetPassiveInitializer()
    {
        PlayerBase player = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();

        passiveActions = new Dictionary<PassiveType, Action<PlayerBase>>
        {
            { PassiveType.Passive_1, (player) => NormalTimeSetting(1.1f) },
            { PassiveType.Passive_2, (player) => NormalTimeSetting(0.9f) },
            { PassiveType.Passive_3, (player) => AttackSetting(player, 1f) },
            { PassiveType.Passive_4, (player) => AttackSetting(player, -0.5f) },
            { PassiveType.Passive_5, (player) => HitSetting(player, 2f) },
            { PassiveType.Passive_6, (player) => HitSetting(player, 4f) },
            { PassiveType.Passive_7, (player) => HitSetting(player, -1f) },
            { PassiveType.Passive_8, (player) => GravitySetting(player, 3.5f) },
            { PassiveType.Passive_9, (player) => GravitySetting(player, 2.5f) },
            { PassiveType.Passive_10, (player) => JumpCountSetting(player, 0) },
            { PassiveType.Passive_11, (player) => JumpCountSetting(player, 2) },
            { PassiveType.Passive_12, (player) => JumpCountSetting(player, 3) },
            { PassiveType.Passive_13, (player) => MaxTimeSetting(130f) },
            { PassiveType.Passive_14, (player) => MaxTimeSetting(110f) },
            { PassiveType.Passive_15, (player) => ExSetting(player, 4f) },
            { PassiveType.Passive_16, (player) => ExSetting(player, 2.5f) },

            { PassiveType.Passive_17, (player) =>
                TimeBuff(5f,
                () => { AttackSetting(player, 2f); },
                () => { AttackSetting(player, -2f); }) },

            { PassiveType.Passive_18, (player) =>
                TimeBuff(10f,
                () => { AttackSetting(player, -1f); },
                () => { AttackSetting(player, 1f); }) },

            { PassiveType.Passive_19, (player) =>
                TimeBuff(10f,
                () => { HitSetting(player, 2f); },
                () => { HitSetting(player, -2f); }) },

            { PassiveType.Passive_20, (player) =>
                TimeBuff(10f,
                () => { HitSetting(player,-1f); },
                () => { HitSetting(player, 1f); }) },

            { PassiveType.Passive_21, (player) => EnergeOperation(0.5f) },

            { PassiveType.Passive_22, (player) =>
                ProbabilityBuff(0.08f,
                () => { EnergeOperation(0.1f); }) },

            { PassiveType.Passive_23, (player) =>
                ProbabilityBuff(0.05f,
                () => { EnergeOperation(0.1f); }) },

            { PassiveType.Passive_24, (player) =>
                ProbabilityBuff(0.05f,
                () => { EnergeOperation(-0.1f); }) },

            { PassiveType.Passive_25, (player) =>
                ProbabilityBuff(0.08f,
                () => { TimeOperation(20f); }) },

            { PassiveType.Passive_26, (player) =>
                LastBuff(() => { TimeOperation(15f); }) },

            { PassiveType.Passive_27, (player) =>
                LastBuff(() => { TimeOperation(30f); }) },

            { PassiveType.Passive_28, (player) =>
                LastBuff(() => { TimeOperation(60f); }) },
        };

        var passiveList = Resources.LoadAll<PassiveData>("PassiveDatas");

        foreach (var passive in passiveList)
        {
            if (passiveActions.TryGetValue(passive.passiveType, out var action))
            {
                passive.SetPassiveAction(action);
            }
            else
            {
                Debug.LogWarning($"Behavior is not defined for passive type {passive.passiveType}.");
            }
        }
    }

    #region Setting
    private void AttackSetting(PlayerBase player, float value)
    {
        player.attackProduct += value;
    }
    private void HitSetting(PlayerBase player, float value)
    {
        player.hitProduct += value;
    }
    private void ExSetting(PlayerBase player, float product)
    {
        player.exAttackProduct = product;
    }
    private void JumpCountSetting(PlayerBase player, int value)
    {
        player.jumpCount = value;
    }
    private void GravitySetting(PlayerBase player, float value)
    {
        player.originGravity = value;
    }
    private void NormalTimeSetting(float product)
    {
        TimeSystem.Instance.SettingTimeProduct(product);
    }
    private void MaxTimeSetting(float value)
    {
        TimeSystem.Instance.SettingMaxTime(value);
    }
    #endregion

    #region Opreation
    private void EnergeOperation(float value)
    {
        if (value == 0) return;

        UISystem.Instance.EXGaugeBar.PlusGauge(value);
    }
    private void TimeOperation(float value)
    {
        if (value == 0) return;

        if (value > 0)
            TimeSystem.Instance.PlusTime(value);
        else
            TimeSystem.Instance.MinusTime(value);
    }
    #endregion

    #region Buff
    private void TimeBuff(float time, Action startAction, Action endAction)
    {
        StartCoroutine(TimebuffCoroutine(time, startAction, endAction));
    }
    private void ProbabilityBuff(float probability, Action action)
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        if (randomValue < probability)
        {
            action?.Invoke();
        }
    }

    private bool isUseBuff;
    private void LastBuff(Action action)
    {
        if (isUseBuff || TimeSystem.Instance.NowTime > 30f) return;
        isUseBuff = true;

        action?.Invoke();
    }
    #endregion

    #region Coroution
    private IEnumerator TimebuffCoroutine(float time, Action startAction, Action endAction)
    {
        startAction?.Invoke();
        yield return new WaitForSeconds(time);
        endAction?.Invoke();
    }
    #endregion
}