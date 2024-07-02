using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;

    public event Action<bool> OnEXTriggerEvt;

    public bool isEX { get; private set; }
    public bool isDeath { get; private set; }

    [SerializeField] private RectTransform gameoverPanel;
    [SerializeField] private TextMeshProUGUI highSocre_timeText;
    [SerializeField] private TextMeshProUGUI highSocre_killText;
    [SerializeField] private Button mainBtn;
    [SerializeField] private Image fadeImage;

    private float highSocre_time = 0f;
    private int highSocre_kill = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TimeSystem.Instance.OnGameoverEvt += GetDeath;
        TimeSystem.Instance.OnGameoverEvt += GameoverPanel;

        AudioManager.Instance.StartBgm("InGame");

        mainBtn.onClick.AddListener(FadeInScene);

        fadeImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        highSocre_time += Time.deltaTime;
    }

    public void SetEX(bool value)
    {
        isEX = value;
        OnEXTriggerEvt?.Invoke(value);
    }

    public void GetKillCount()
    {
        highSocre_kill++;
    }

    private void GetDeath()
    {
        isDeath = true;
    }

    private void GameoverPanel()
    {
        AudioManager.Instance.StartSfx("GameEnd");

        highSocre_timeText.text = $"Time : {highSocre_time}";
        highSocre_killText.text = $"Kill : {highSocre_kill}";

        gameoverPanel.DOMoveY(540f, 0.5f).SetEase(Ease.OutBounce);
    }

    private void FadeInScene()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, 1f).OnComplete(() => 
        {
            SceneManager.LoadScene("Intro");
        });
    }

    public bool IsStopLogic()
    {
        return isDeath || isEX;
    }
}
