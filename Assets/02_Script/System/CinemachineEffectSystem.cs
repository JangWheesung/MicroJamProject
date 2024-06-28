using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CinemachineEffectSystem : MonoBehaviour
{
    public static CinemachineEffectSystem Instance;

    [SerializeField] private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noiseCam;

    private Sequence camSequence;

    private void Awake()
    {
        Instance = this;
        noiseCam = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void CinemachineShaking()
    {
        StartCoroutine(ShakeCor());
    }

    IEnumerator ShakeCor()
    {
        float rotateZ = Random.Range(-3f, 3f);
        while (Mathf.Abs(rotateZ) < 2f)
        {
            rotateZ = Random.Range(-3f, 3f);
        }

        camSequence.Kill();
        camSequence = DOTween.Sequence();
        camSequence
            .Append(vcam.transform.DORotate(new Vector3(0, 0, rotateZ), 0.1f).SetEase(Ease.OutBack))
            .Append(vcam.transform.DORotate(Vector3.zero, 0.3f));

        noiseCam.m_AmplitudeGain = 5;
        yield return new WaitForSeconds(0.4f);
        noiseCam.m_AmplitudeGain = 0;
    }
}
