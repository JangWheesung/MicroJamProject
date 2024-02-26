using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class AttackEffectSystem : MonoBehaviour
{
    public static AttackEffectSystem Instance;

    [SerializeField] private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noiseCam;

    [SerializeField] private CircleEffect circleEffect;
    [SerializeField] private SmashingEffect smashingEffect;

    private Sequence camSequence;

    private void Awake()
    {
        Instance = this;
        noiseCam = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void CinemachineShaking(Transform pos)
    {
        StartCoroutine(ShakeCor());
    }

    public void CircleEffect(Transform pos)
    {
        PoolingManager.instance.Pop<CircleEffect>(circleEffect.name, pos.position);
    }

    public void SmashingEffect(Transform pos)
    {
        PoolingManager.instance.Pop<SmashingEffect>(smashingEffect.name, pos.position);
    }

    public void SoundEffect(Transform pos)
    {
        int random = Random.Range(1, 4);
        AudioManager.Instance.StartSfx($"Smashing{random}");
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
