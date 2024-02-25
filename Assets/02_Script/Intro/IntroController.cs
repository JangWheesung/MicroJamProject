using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;

public class IntroController : MonoBehaviour
{
    [Header("Fade")]
    [SerializeField] private RectTransform buttonPanel;
    [SerializeField] private Image fadeImage;

    [Header("Play")]
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private ParticleSystem introParticle;
    [SerializeField] private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noiseCam;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Transform ground;

    [Header("HowToPlay")]
    [SerializeField] private GameObject howtoplayPanel;

    private void Start()
    {
        noiseCam = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        fadeImage.DOFade(0, 0.5f).OnComplete(() => { fadeImage.gameObject.SetActive(false); });
        buttonPanel.DOMoveX(50, 0.5f).SetEase(Ease.OutBack);

        AudioManager.Instance.StartBgm("Intro");
    }

    public void Play()
    {
        introParticle.loop = false;
        introParticle.gravityModifier = 5;

        noiseCam.m_AmplitudeGain = 0;

        mainText.transform.DOMoveX(-1000, 0.5f).SetEase(Ease.InBack);

        buttonPanel.DOMoveX(-500, 0.5f).SetEase(Ease.InBack).OnComplete(() => 
        {
            timeText.transform.DOMoveY(1080, 0.5f);
            ground.DOMoveY(-5, 0.5f).OnComplete(() => 
            {
                SceneManager.LoadScene("Wheesong"); 
            });
        });
    }

    public void HowToPlay()
    {
        howtoplayPanel.SetActive(!howtoplayPanel.activeSelf);
    }

    public void Quit()
    {
        buttonPanel.DOMoveX(-500, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            Application.Quit();
        });
    }
}
