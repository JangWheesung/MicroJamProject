using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameoverUI : MonoBehaviour
{
    [SerializeField] private RectTransform gameoverPanel;
    [SerializeField] private TextMeshProUGUI highSocre_timeText;
    [SerializeField] private TextMeshProUGUI highSocre_killText;
    [SerializeField] private Button mainBtn;
    [SerializeField] private Image fadeImage;

    private void Start()
    {
        mainBtn.onClick.AddListener(FadeInScene);
    }

    public void GameoverPanel(float time, float kill)
    {
        AudioManager.Instance.StartSfx("GameEnd");

        highSocre_timeText.text = $"Time : {time}";
        highSocre_killText.text = $"Kill : {kill}";

        gameoverPanel.DOMoveY(0f, 0.5f).SetEase(Ease.OutBounce);
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
