using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] private Image slotImage;
    [SerializeField] private Transform skillTrs;
    [SerializeField] private Transform passiveTrs;

    [Header("StatData")]
    [SerializeField] private TMP_Text skillNameText;
    [SerializeField] private TMP_Text skillExtText;
    [SerializeField] private TMP_Text skillDelayText;
    [SerializeField] private TMP_Text passiveNameText;
    [SerializeField] private RectTransform passivePanelContext;

    [Header("Prefab")]
    [SerializeField] private PassiveSlot passiveSlotObj;

    private SelectUIController characterController;
    private CharacterStat characterStat;

    public void SetSlot(CharacterStat stat, SelectUIController controller)
    {
        characterController = controller;
        characterStat = stat;
        slotImage.sprite = stat.characterSprite;

        SetSkillData(stat);
        SetPassiveData(stat);
    }

    private void SetSkillData(CharacterStat stat)
    {
        skillNameText.color = stat.characterColor;
        skillNameText.text = stat.skillName;
        skillExtText.text = stat.skillExt;
        skillDelayText.text = $"스킬 쿨타임 : {stat.skillDelayStat}s";
    }

    private void SetPassiveData(CharacterStat stat)
    {
        int passiveCnt = 0;
        foreach (PassiveData passiveData in stat.passiveDatas)
        {
            PassiveSlot slot = Instantiate(passiveSlotObj, passivePanelContext);
            slot.SetSlot(passiveData.passiveName, passiveData.passiveExt);

            passiveCnt++;
        }

        passiveNameText.text = $"패시브 ({passiveCnt}/4)";
    }

    public void OnSlotClick()
    {
        characterController.SeEntryPanel(this, characterStat, transform.position);

        SetButtonMove(skillTrs, -1.4f);
        SetButtonMove(passiveTrs, 1.4f);
    }

    public void CloseSlot()
    {
        SetButtonMove(skillTrs, 0f);
        SetButtonMove(passiveTrs, 0f);
    }

    private void SetButtonMove(Transform trs, float value)
    {
        trs.DOLocalMoveX(value, 0.2f).SetEase(Ease.OutExpo);
    }
}
