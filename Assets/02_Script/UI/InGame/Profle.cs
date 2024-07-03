using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Profle : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image characterImage;

    public void ProfleSetting(Color color, Sprite sprite)
    {
        backgroundImage.color = color;
        characterImage.sprite = sprite;
    }

    public void PopProfle(float time)
    {
        Vector2 backgroundOrgPos = transform.localPosition;
        Vector2 characterOrgPos = characterImage.transform.localPosition;
        Quaternion characterOrgRot = characterImage.transform.localRotation;

        Vector2 backgroundPos = new Vector2(-1000, 550);
        Vector2 characterPos = new Vector2(0, -80);
        Quaternion characterRot = new Quaternion(-20, 180, 0, 0);

        DOTween.Sequence()
            .Append(MoveToProfleSequence(backgroundPos, characterPos, characterRot, time))
            .Append(MoveToProfleSequence(backgroundOrgPos, characterOrgPos, characterOrgRot, time));
    }

    private Sequence MoveToProfleSequence(Vector2 bPos, Vector2 cPos, Quaternion cRot, float t)
    {
        return DOTween.Sequence()
            .Append(transform.DOLocalMove(bPos, t).SetEase(Ease.OutBack))
            .Join(characterImage.transform.DOLocalMove(cPos, t).SetEase(Ease.OutBack))
            .Join(characterImage.transform.DOLocalRotateQuaternion(cRot, t).SetEase(Ease.OutBack));
    }
}
