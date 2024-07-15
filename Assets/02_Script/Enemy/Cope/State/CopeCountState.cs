using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopeCountState : CopeBaseState
{
    private Coroutine colorCor;
    private float currentTime;

    public CopeCountState(CopeFSM fsm) : base(fsm)
    {
    }

    protected override void OnStateEnter()
    {
        colorCor = StartCoroutine(ColorBright());

        PoolingManager.Instance.Pop<EffectBase>(bombRangeEffect.name, copeFSM.transform.position).PopEffect();
    }

    protected override void OnStateUpdate()
    {
        currentTime += Time.deltaTime;
        countText.text = (countDelay - currentTime).ToString("F1");
    }

    protected override void OnStateExit()
    {
        StopCoroutine(colorCor);

        sp.color = Color.white;

        currentTime = 0f;
        countText.text = "";
    }

    private IEnumerator ColorBright()
    {
        float currentTime = 0.3f;
        float initialTime = 0.7f;
        float delayTime = 0.05f;

        float time = initialTime;
        float twoThirdsDelay = countDelay * 2 / 3;

        while (currentTime < countDelay)
        {
            sp.color = Color.white;
            yield return new WaitForSeconds(time);

            sp.color = Color.red;
            yield return new WaitForSeconds(delayTime);

            currentTime += (time + delayTime);

            if (currentTime < twoThirdsDelay)
                time = Mathf.Max(time - 0.2f, delayTime);
            else
                time = delayTime;

            AudioManager.Instance.StartSfx("Alarm", 0.6f);
        }
    }
}
