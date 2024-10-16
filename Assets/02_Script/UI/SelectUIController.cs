using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SelectUIController : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private TMP_Text timeObj;

    [Header("PrefabObj")]
    [SerializeField] private CharacterSlot characterSlotObj;
    [SerializeField] private PassiveSlot abilitySlotObj;

    [Header("Panel")]
    [SerializeField] private RectTransform selectRoot;
    [SerializeField] private RectTransform leftRoot;
    [SerializeField] private RectTransform rightRoot;
    [SerializeField] private RectTransform topRoot;
    [SerializeField] private RectTransform buttonRoot;
    private Image statPanel;
    private Image attackPanel;

    [Header("UIRoot")]
    [SerializeField] private RectTransform characterPanelTrs;

    [Header("Name")]
    [SerializeField] private TMP_Text nameText;

    [Header("Stat")]
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Slider jumpSlider;

    [Header("AttackText")]
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text attackDelayText;
    [SerializeField] private TMP_Text exNameText;
    [SerializeField] private TMP_Text exExtText;

    private string choiceCharacterName;
    private CharacterSlot currentSlot;
    private Camera cam;

    private void Awake()
    {
        CharacterStat[] characterStats = Resources.LoadAll<CharacterStat>("CharcterStats");

        foreach (CharacterStat stat in characterStats)
        {
            CharacterSlot slot = Instantiate(characterSlotObj, characterPanelTrs);
            slot.SetSlot(stat, this);
        }
        
        statPanel = buttonRoot.Find("StatPanel").GetComponent<Image>();
        attackPanel = buttonRoot.Find("AttackPanel").GetComponent<Image>();

        cam = Camera.main;
    }

    private void Start()
    {
        selectRoot.DOLocalMoveY(0f, 0.5f).SetEase(Ease.OutBack);
    }

    public void SeEntryPanel(CharacterSlot slot, CharacterStat stat, Vector2 slotPos)
    {
        currentSlot = slot;

        TextSetting(stat); //텍스트 세팅
        //AbilityPanelSetting(stat.passiveDatas); //특성 UI 세팅
        PanelColorSetting(stat.characterColor); //UI 색깔 조정

        FocusMove(slotPos); //카메라 무빙
        PanelMove(true); //UI 무빙
    }

    private void TextSetting(CharacterStat stat)
    {
        nameText.color = stat.characterColor;
        nameText.text = stat.chatacterName;
        choiceCharacterName = stat.chatacterName;

        speedSlider.value = stat.speedStat;
        jumpSlider.value = stat.jumpStat;

        attackText.text = $"\"{stat.attackExt}\"";
        attackDelayText.text = $"공격 딜레이 : {stat.attackDelayStat}s";

        exNameText.color = stat.characterColor;
        exNameText.text = stat.exName;
        exExtText.color = stat.characterColor;
        exExtText.text = stat.exExt;
    }

    //private void AbilityPanelSetting(PassiveData[] datas)
    //{
    //    for (int i = 0; i < abilityPanelContext.childCount; i++)
    //    {
    //        Destroy(abilityPanelContext.GetChild(i).gameObject);
    //    }
    //    abilityPanelContext.sizeDelta = Vector2.zero;

    //    foreach (PassiveData abilityData in datas)
    //    {
    //        AbilitySlot slot = Instantiate(abilitySlotObj, abilityPanelContext);
    //        slot.SetSlot(abilityData.passiveName, abilityData.passiveExt);

    //        abilityPanelContext.sizeDelta += new Vector2(320f, 0f);
    //    }
    //}

    private void PanelColorSetting(Color color)
    {
        //namePanel.color = color;
        attackPanel.color = color;
        statPanel.color = color;
    }

    public void OnBackClick()
    {
        currentSlot.CloseSlot();

        FocusMove();
        PanelMove(false);
    }

    public void OnChoiceCilck()
    {
        if (choiceCharacterName == null) return;

        PlayerPrefs.SetString("PlayerName", choiceCharacterName);

        selectRoot.DOLocalMoveY(10f, 0.2f).SetEase(Ease.InQuart);
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
        pos.z = -10f;
        cam.transform.DOLocalMove(pos, 0.2f).SetEase(Ease.OutExpo);
        cam.DOOrthoSize(1.5f, 0.2f).SetEase(Ease.OutExpo);
    }

    private void PanelMove(bool pop, Action endEvt = null)
    {
        if (pop)
        {
            leftRoot.DOLocalMoveX(0, 0.3f).SetEase(Ease.OutBack);
            rightRoot.DOLocalMoveX(0, 0.3f).SetEase(Ease.OutBack);
            topRoot.DOLocalMoveY(0, 0.3f).SetEase(Ease.OutBack);
            buttonRoot.DOLocalMoveY(0, 0.3f).SetEase(Ease.OutBack)
                .OnComplete(() => endEvt?.Invoke());
        }
        else
        {
            leftRoot.DOLocalMoveX(-1800, 0.3f).SetEase(Ease.OutBack);
            rightRoot.DOLocalMoveX(1000, 0.3f).SetEase(Ease.OutBack);
            topRoot.DOLocalMoveY(800f, 0.3f).SetEase(Ease.OutBack);
            buttonRoot.DOLocalMoveY(-250, 0.3f).SetEase(Ease.OutBack)
                .OnComplete(() => endEvt?.Invoke());
        }
    }
}