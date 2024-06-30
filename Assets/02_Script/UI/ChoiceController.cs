using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class ChoiceController : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private SpriteRenderer characterSp;
    [SerializeField] private TMP_Text timeObj;

    [Header("Prefab")]
    [SerializeField] private CharacterSlot characterSlotObj;
    [SerializeField] private AbilitySlot abilitySlotObj;

    [Header("Panel")]
    [SerializeField] private RectTransform upPanel;
    [SerializeField] private RectTransform leftPanel;
    [SerializeField] private RectTransform rightPanel;

    [Header("UI")]
    [SerializeField] private RectTransform characterPanelTrs;
    [SerializeField] private RectTransform abilityPanelContext;

    [Header("StatPanel")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text jumpText;
    [SerializeField] private TMP_Text dashText;
    [SerializeField] private TMP_Text attackText;

    private string choiceCharacterName;

    private void Awake()
    {
        CharacterStat[] characterStats = Resources.LoadAll<CharacterStat>("CharcterStats");

        foreach (CharacterStat stat in characterStats)
        {
            CharacterSlot slot = Instantiate(characterSlotObj, characterPanelTrs);
            slot.SetSlot(stat, this);
        }
    }

    private void Start()
    {
        PanelMove(0);
    }

    private void PanelMove(float root, Action endEvt = null)
    {
        upPanel.DOLocalMoveY(root, 0.5f).SetEase(Ease.OutBack);
        leftPanel.DOLocalMoveX(-root, 0.5f).SetEase(Ease.OutBack);
        rightPanel.DOLocalMoveX(root, 0.5f).SetEase(Ease.OutBack)
            .OnComplete(() => endEvt?.Invoke());
    }

    public void SeEntryPanel(CharacterStat stat)
    {
        characterSp.sprite = stat.characterSprite;

        nameText.color = stat.characterColor;
        nameText.text = stat.chatacterName;
        choiceCharacterName = stat.chatacterName;

        speedText.text = "스피드 : " + stat.speedStat switch
        {
            SpeedStat.Slow => "<color=blue>느림",
            SpeedStat.Normal => "<color=green>보통",
            SpeedStat.Fast => "<color=red>빠름",
            _ => "X"
        };
        jumpText.text = "점프 : " + (stat.jumpStat <= 0 ? "X" : $"{stat.jumpStat}단");
        dashText.text = "대시 : " + (stat.dashStat <= 0f ? "X" : $"x {stat.dashStat}");
        attackText.text = "공격 : " + stat.attackExt;

        for (int i = 0; i < abilityPanelContext.childCount; i++)
        {
            Destroy(abilityPanelContext.GetChild(i).gameObject);
        }

        abilityPanelContext.sizeDelta = Vector2.zero;

        foreach (AbilityData abilityData in stat.abilityDatas)
        {
            AbilitySlot slot = Instantiate(abilitySlotObj, abilityPanelContext);
            slot.SetSlot(abilityData.name, abilityData.ext);

            switch (abilityData.abilityStat)
            {
                case AbilityStat.Jump:
                    jumpText.text = "점프 : " + $"[{abilityData.name}]";
                    break;

                case AbilityStat.Dash:
                    dashText.text = "대시 : " + $"[{abilityData.name}]";
                    break;

                case AbilityStat.Attack:
                    string extRemake = attackText.text;
                    extRemake.Replace($"{abilityData.name}", $"<#0080FF>[{abilityData.name}]<color=white>");
                    attackText.text = extRemake;
                    break;
            }

            abilityPanelContext.sizeDelta += new Vector2(0, 300f);
        }
    }

    public void OnChoiceCilck()
    {
        if (choiceCharacterName == null) return;

        PlayerPrefs.SetString("PlayerName", choiceCharacterName);

        characterSp.transform.DOMoveY(10f, 0.4f);
        timeObj.DOFade(1, 0.4f);
        PanelMove(600, () => 
        {
            SceneManager.LoadScene(SceneList.InGame);
        });
    }
}