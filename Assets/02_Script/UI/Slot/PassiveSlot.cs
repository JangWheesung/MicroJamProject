using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PassiveSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text extText;

    public void SetSlot(string name, string ext)
    {
        nameText.text = $"[{name}]";
        extText.text = ext;
    }
}
