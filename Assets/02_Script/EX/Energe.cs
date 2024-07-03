using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Energe : EffectBase
{
    [Header("Energe")]
    [SerializeField] private EffectBase energeParticle;
    [SerializeField] private float moveTime;
    [SerializeField] private float radius;
    [SerializeField] private float erengeAmout;
    [SerializeField] private LayerMask playerLayer;

    private EXGaugeBar gaugeBar;
    private bool getEnerge;

    protected override void OnEnable()
    {
        getEnerge = false;

        base.OnEnable();
    }

    public void PopEnerge(EXGaugeBar gaugeBar)
    {
        this.gaugeBar = gaugeBar;

        AudioManager.Instance.StartSfx("EnergeUp");
    }

    private void Update()
    {
        bool InPlayer = Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        if (InPlayer && !getEnerge)
        {
            getEnerge = true;

            if (lifeCor != null)
                StopCoroutine(lifeCor);

            AudioManager.Instance.StartSfx("EnergeDown");

            PoolingManager.Instance.Pop<EffectBase>(energeParticle.name, transform.position).PopEffect();

            SpecialEffectSystem.Instance.BackgroundAura(Color.gray);
            SpecialEffectSystem.Instance.BloomIntensity(BloomType.Light_M);
            SpecialEffectSystem.Instance.CameraShaking(CameraType.ZoomOut);

            StartCoroutine(MoveToGaugeBar());
        }
    }

    private void EndMove()
    {
        gaugeBar.PlusGauge(erengeAmout);
        PoolingManager.Instance.Push(gameObject);
    }

    #region ReturnVecter

    private Vector3 GetControlPoint(Vector3 start, Vector3 end)
    {
        Vector3 midPoint = (start + end) / 2; //중간점
        Vector3 direction = (end - start).normalized; //게이지바 방향
        Vector3 perpendicular = new Vector3(-direction.y, direction.x).normalized; //수직인 방향(2D)

        float randomOffset = Random.Range(1f, 2f);
        Vector3 controlPoint = midPoint + perpendicular * randomOffset;

        return controlPoint;
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // 2차 베지어 곡선 방정식
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // (1-t)^2 * P0
        p += 2 * u * t * p1; // 2 * (1-t) * t * P1
        p += tt * p2; // t^2 * P2

        return p;
    }

    private Vector3 GetWorldPosition(Vector2 screenPoint)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPoint);
        return worldPosition;
    }

    #endregion

    private IEnumerator MoveToGaugeBar()
    {
        Vector3 startPosition = transform.position; //시작 위치
        Vector3 endPosition = GetWorldPosition(gaugeBar.GetIconPos()); //끝 위치
        Vector3 controlPoint = GetControlPoint(startPosition, endPosition); // 중간 위치

        float t = 0f;
        DOTween.To(() => t, x => t = x, 1f, moveTime).SetEase(Ease.InBack);

        while (t < 1f)
        { 
            // 베지어 곡선을 따라 이동
            Vector3 position = CalculateBezierPoint(t, startPosition, controlPoint, endPosition);
            transform.position = position;

            yield return null;
        }
        
        EndMove();
        yield return null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
#endif
}
