using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] private Image slotImage;
    private ChoiceController characterController;
    private CharacterStat characterStat;

    public void SetSlot(CharacterStat stat, ChoiceController controller)
    {
        characterController = controller;
        characterStat = stat;
        slotImage.sprite = stat.characterSprite;
    }

    public void OnSlotClick()
    {
        characterController.SeEntryPanel(characterStat, transform.position);
    }
}
