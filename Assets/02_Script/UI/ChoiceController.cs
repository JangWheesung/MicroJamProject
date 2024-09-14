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
    [SerializeField] private RectTransform selectRoot;
    [SerializeField] private RectTransform leftRoot;
    [SerializeField] private RectTransform rightRoot;
    [SerializeField] private RectTransform topRoot;
    [SerializeField] private RectTransform buttonRoot;
    //private Image namePanel;
    private Image statPanel;
    private Image attackPanel;
    //private Image abilityPanel;

    [Header("UIRoot")]
    [SerializeField] private RectTransform characterPanelTrs;
    [SerializeField] private RectTransform abilityPanelContext;

    [Header("Name")]
    [SerializeField] private TMP_Text nameText;

    [Header("Stat")]
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Slider jumpSlider;
    [SerializeField] private Slider dashSlider;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text jumpText;
    [SerializeField] private TMP_Text dashText;

    [Header("AttackText")]
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

        //namePanel = topRoot.Find("NamePanel").GetComponent<Image>();
        statPanel = buttonRoot.Find("StatPanel").GetComponent<Image>();
        attackPanel = buttonRoot.Find("AttackPanel").GetComponent<Image>();
        //abilityPanel = rightRoot.Find("AbilityPanel").GetComponent<Image>();

        cam = Camera.main;
    }

    private void Start()
    {
        selectRoot.DOLocalMoveY(0f, 0.5f).SetEase(Ease.OutBack);
    }

    public void SeEntryPanel(CharacterStat stat, Vector2 slotPos)
    {
        TextSetting(stat); //텍스트 세팅
        AbilityPanelSetting(stat.abilityDatas); //특성 UI 세팅
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
        dashSlider.value = stat.skillDelayStat;

        speedText.text = stat.speedStat.ToString();
        jumpText.text = stat.jumpStat.ToString();
        dashText.text = stat.skillDelayStat.ToString();

        attackText.text = $"\"{stat.attackExt}\"";
        
        exNameText.color = stat.characterColor;
        exNameText.text = stat.exName;
        exExtText.color = stat.characterColor;
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
            slot.SetSlot(abilityData.name, abilityData.ext);

            abilityPanelContext.sizeDelta += new Vector2(320f, 0f);
        }
    }

    private void PanelColorSetting(Color color)
    {
        //namePanel.color = color;
        attackPanel.color = color;
        statPanel.color = color;
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