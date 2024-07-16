using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ChoiceController : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private TMP_Text timeObj;

    [Header("PrefabObj")]
    [SerializeField] private CharacterSlot characterSlotObj;
    [SerializeField] private AbilitySlot abilitySlotObj;

    [Header("Panel")]
    [SerializeField] private RectTransform selectPanel;
    [SerializeField] private RectTransform leftPanel;
    [SerializeField] private RectTransform rightPanel;
    [SerializeField] private RectTransform buttonPanel;
    private Image namePanel;
    private Image statPanel;
    private Image attackPanel;
    private Image exPanel;
    private Image abilityPanel;

    [Header("UIRoot")]
    [SerializeField] private RectTransform characterPanelTrs;
    [SerializeField] private RectTransform abilityPanelContext;

    [Header("StatText")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text jumpText;
    [SerializeField] private TMP_Text dashText;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text exNameText;
    [SerializeField] private TMP_Text exExtText;

    private string choiceCharacterName;
    private Camera cam;

    private void Awake()
    {
        CharacterStat[] characterStats = Resources.LoadAll<CharacterStat>("CharcterStats");

        foreach (CharacterStat stat in characterStats)
        {
            CharacterSlot slot = Instantiate(characterSlotObj, characterPanelTrs);
            slot.SetSlot(stat, this);
        }

        namePanel = leftPanel.Find("NamePanel").GetComponent<Image>();
        statPanel = rightPanel.Find("StatPanel").GetComponent<Image>();
        attackPanel = rightPanel.Find("AttackPanel").GetComponent<Image>();
        exPanel = rightPanel.Find("EXPanel").GetComponent<Image>();
        abilityPanel = rightPanel.Find("AbilityPanel").GetComponent<Image>();

        cam = Camera.main;
    }

    private void Start()
    {
        selectPanel.DOLocalMoveY(0f, 0.5f).SetEase(Ease.OutBack);
    }

    public void SeEntryPanel(CharacterStat stat, Vector2 slotPos)
    {
        TextSetting(stat);
        AbilityPanelSetting(stat.abilityDatas);
        PanelColorSetting(stat.characterColor);

        FocusMove(slotPos);
        PanelMove(true);
    }

    private void TextSetting(CharacterStat stat)
    {
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
        dashText.text = "대시 : " + (stat.dashStat <= 0f ? "X" : $"x{stat.dashStat.ToString("F1")}");
        attackText.text = stat.attackExt;
        exNameText.color = stat.characterColor;
        exNameText.text = stat.exName;
        exExtText.text = stat.exExt;
    }

    private void AbilityPanelSetting(AbilityData[] datas)
    {
        for (int i = 0; i < abilityPanelContext.childCount; i++)
        {
            Destroy(abilityPanelContext.GetChild(i).gameObject);
        }
        abilityPanelContext.sizeDelta = Vector2.zero;

        foreach (AbilityData abilityData in datas)
        {
            AbilitySlot slot = Instantiate(abilitySlotObj, abilityPanelContext);
            slot.SetSlot(abilityData.abilityStat, abilityData.name, abilityData.ext);

            string abilityName = $"<#0080FF>[{abilityData.name}]<color=white>";
            switch (abilityData.abilityStat)
            {
                case AbilityStat.Jump:
                    jumpText.text = "점프 : " + abilityName;
                    break;

                case AbilityStat.Dash:
                    dashText.text = "대시 : " + abilityName;
                    break;

                case AbilityStat.Attack:
                    string extRemake = attackText.text;
                    extRemake.Replace($"{abilityData.name}", abilityName);
                    attackText.text = extRemake;
                    break;
            }

            abilityPanelContext.sizeDelta += new Vector2(350f, 0f);
        }
    }

    private void PanelColorSetting(Color color)
    {
        namePanel.color = color;
        attackPanel.color = color;
        exPanel.color = NewColor(color);
        statPanel.color = NewColor(exPanel.color);
        abilityPanel.color = NewColor(statPanel.color);
    }

    private Color NewColor(Color color)
    {
        color.r -= 0.2f;
        color.b -= 0.2f;
        color.g -= 0.2f;

        return color;
    }

    public void OnBackClick()
    {
        FocusMove();
        PanelMove(false);
    }

    public void OnChoiceCilck()
    {
        if (choiceCharacterName == null) return;

        PlayerPrefs.SetString("PlayerName", choiceCharacterName);

        selectPanel.DOLocalMoveY(10f, 0.2f).SetEase(Ease.InQuart);
        timeObj.DOFade(1, 0.2f);

        FocusMove();
        PanelMove(false, () => 
        {
            SceneManager.LoadScene(SceneList.InGame);
        });
    }

    private void FocusMove()
    {
        cam.transform.DOLocalMove(new Vector3(0, 0, -10f), 0.2f).SetEase(Ease.OutExpo);
        cam.DOOrthoSize(5f, 0.2f).SetEase(Ease.OutExpo);
    }

    private void FocusMove(Vector3 pos)
    {
        pos.x += 1f;
        pos.z = -10f;
        cam.transform.DOLocalMove(pos, 0.2f).SetEase(Ease.OutExpo);
        cam.DOOrthoSize(1.5f, 0.2f).SetEase(Ease.OutExpo);
    }

    private void PanelMove(bool pop, Action endEvt = null)
    {
        if (pop)
        {
            leftPanel.DOLocalMoveX(0, 0.3f).SetEase(Ease.OutBack);
            rightPanel.DOLocalMoveX(0, 0.3f).SetEase(Ease.OutBack);
            buttonPanel.DOLocalMoveY(0, 0.3f).SetEase(Ease.OutBack)
                .OnComplete(() => endEvt?.Invoke());
        }
        else
        {
            leftPanel.DOLocalMoveX(-800, 0.3f).SetEase(Ease.OutBack);
            rightPanel.DOLocalMoveX(1000, 0.3f).SetEase(Ease.OutBack);
            buttonPanel.DOLocalMoveY(-250, 0.3f).SetEase(Ease.OutBack)
                .OnComplete(() => endEvt?.Invoke());
        }
    }
}