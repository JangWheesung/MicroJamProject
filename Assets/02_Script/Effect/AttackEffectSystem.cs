using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AttackEffectSystem : MonoBehaviour
{
    public static AttackEffectSystem Instance;

    [SerializeField] private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noiseCam;

    [SerializeField] private CircleEffect circleEffect;

    [SerializeField] private SmashingEffect smashingEffect;

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

    IEnumerator ShakeCor()
    {
        noiseCam.m_AmplitudeGain = 6;
        yield return new WaitForSeconds(0.4f);
        noiseCam.m_AmplitudeGain = 0;
    }
}
