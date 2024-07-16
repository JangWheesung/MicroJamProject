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
    [SerializeField] private Image fadeImage;

    [Header("Particle")]
    [SerializeField] private ParticleSystem introParticle;
    [SerializeField] private Sprite[] particleSprites;

    [Header("Play")]
    [SerializeField] private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noiseCam;
    [SerializeField] private TextMeshProUGUI pressText;
    [SerializeField] private Transform mainTrs;
    [SerializeField] private Transform ground;

    private bool isStart;

    private void Start()
    {
        noiseCam = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        fadeImage.DOFade(0, 0.5f).OnComplete(() => 
        {
            fadeImage.gameObject.SetActive(false);
        });
        pressText.DOFade(0f, 0.7f).SetLoops(-1, LoopType.Yoyo);

        Sprite sprite = particleSprites[Random.Range(0, particleSprites.Length)];
        introParticle.textureSheetAnimation.SetSprite(0, sprite);

        AudioManager.Instance.StartBgm("Intro");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isStart)
        {
            isStart = true;
            Play();
        }
    }

    private void Play()
    {
        var mainModule = introParticle.main;
        mainModule.loop = false;
        mainModule.gravityModifier = 5;

        pressText.gameObject.SetActive(false);

        mainTrs.transform.DOMoveY(10, 0.5f).OnComplete(() =>
        {
            noiseCam.m_AmplitudeGain = 0;

            ground.DOMoveY(-5, 0.6f).OnComplete(() =>
            {
                SceneManager.LoadScene(SceneList.Choice);
            });
        });
    }
}
