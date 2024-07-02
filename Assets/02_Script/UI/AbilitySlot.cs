using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilitySlot : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text extText;

    public void SetSlot(AbilityStat stat, string name, string ext)
    {
        if (stat == AbilityStat.EX)
            nameText.color = Color.red;

        nameText.text = $"[{name}]";
        extText.text = ext;
    }
}
