using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class GameoverSystem : MonoBehaviour
{
    public static GameoverSystem Instance;

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
        fadeImage.gameObject.SetActive(false);

        TimeSystem.Instance.OnGameoverEvt += GetDeath;
        TimeSystem.Instance.OnGameoverEvt += GameoverPanel;
        mainBtn.onClick.AddListener(FadeInScene);
    }

    private void Update()
    {
        highSocre_time += Time.deltaTime;
    }

    private void GetDeath()
    {
        isDeath = true;
    }

    private void GameoverPanel()
    {
        highSocre_timeText.text = $"Time : {highSocre_time}";
        highSocre_killText.text = $"Kill : {highSocre_kill}";

        gameoverPanel.DOMoveY(540f, 0.5f).SetEase(Ease.OutBounce);
    }

    public void GetKillCount()
    {
        highSocre_kill++;
    }

    private void FadeInScene()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, 1f).OnComplete(() => 
        {
            SceneManager.LoadScene("Intro");
        });
    }
}
