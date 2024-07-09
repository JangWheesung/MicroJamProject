using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public enum CameraType
{
    Shake_S = 2,
    Shake_M = 3,
    Shake_H = 4,
    Rock_S,
    Rock_H,
    ZoomIn,
    ZoomOut
};

public enum BloomType
{
    Light_S = 5,
    Light_M = 10,
    Light_H = 30,
};

public class SpecialEffectSystem : MonoBehaviour
{
    public static SpecialEffectSystem Instance;

    [SerializeField] private SpriteRenderer exBackgroundSp;
    [SerializeField] private Volume volume;
    [SerializeField] private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noiseCam;
    private Camera cam;

    private void Awake()
    {
        Instance = this;

        noiseCam = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cam = Camera.main;
    }

    public void BackgroundDarkness(float time, bool fadeIn = true)
    {
        float fadeValue = fadeIn ? 0.5f : 0f;
        exBackgroundSp.DOFade(fadeValue, time).SetEase(Ease.OutBack);
    }

    public void BackgroundAura(Color color)
    {
        cam.backgroundColor = color;
        cam.DOColor(Color.black, 0.4f).SetEase(Ease.OutExpo);
    }

    public void BloomIntensity(BloomType bloomType)
    {
        if (volume.profile.TryGet(out Bloom bloom))
        {
            float originValue = bloom.intensity.value;
            bloom.intensity.value *= (int)bloomType;
            DOTween.To(() => bloom.intensity.value, x => bloom.intensity.value = x, originValue, 0.5f).SetEase(Ease.OutExpo);
        }
    }
    
    /// <summary>
    /// 두 번째 인자 time은 RockType일 때만
    /// </summary>
    /// <param name="cameraType"></param>
    /// <param name="rockTime"></param>
    public void CameraShaking(CameraType cameraType ,float rockTime = 0.4f)
    {
        switch (cameraType)
        {
            case CameraType.Shake_S:
            case CameraType.Shake_M:
            case CameraType.Shake_H:
                StartCoroutine(ShakeCor(cameraType));
                break;

            case CameraType.Rock_S:
            case CameraType.Rock_H:
                StartCoroutine(RockCor(cameraType, rockTime));
                break;

            case CameraType.ZoomIn:
            case CameraType.ZoomOut:
                ZoomCor(cameraType);
                break;
        }
    }

    private IEnumerator ShakeCor(CameraType cameraType)
    {
        float shakeAmount = (int)cameraType;

        float rotateZ = 0;
        while (Mathf.Abs(rotateZ) < (shakeAmount / 2f))
        {
            rotateZ = Random.Range(-shakeAmount, shakeAmount);
            yield return null;
        }

        DOTween.Sequence()
            .Append(vcam.transform.DORotate(new Vector3(0, 0, rotateZ), 0.1f).SetEase(Ease.OutBack))
            .Append(vcam.transform.DORotate(Vector3.zero, 0.3f));

        noiseCam.m_AmplitudeGain = shakeAmount * 1.5f;
        yield return new WaitForSeconds(0.4f);
        noiseCam.m_AmplitudeGain = 0;
    }

    private IEnumerator RockCor(CameraType cameraType, float time)
    {
        float rockValue = cameraType == CameraType.Rock_S ? 3 : 6;
        
        noiseCam.m_AmplitudeGain = rockValue;
        yield return new WaitForSeconds(time);
        noiseCam.m_AmplitudeGain = 0;
    }

    private void ZoomCor(CameraType cameraType)
    {
        float sizeValue = cameraType == CameraType.ZoomIn ? 3f : 7f;

        DOTween.Sequence()
            .Append(DOTween.To(() => vcam.m_Lens.OrthographicSize, x => vcam.m_Lens.OrthographicSize = x, sizeValue, 0.3f).SetEase(Ease.OutCubic))
            .Append(DOTween.To(() => vcam.m_Lens.OrthographicSize, x => vcam.m_Lens.OrthographicSize = x, 5f, 0.5f));
    }
}
