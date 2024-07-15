using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeBarObj : MonoBehaviour
{
    [SerializeField] private Transform pivot;

    private float maxValue;
    public float MaxValue 
    {
        get { return maxValue; }
        set { maxValue = value; }
    }

    private float value;
    public float Value
    {
        get
        {
            return value;
        }
        set
        {
            pivot.localScale = new Vector3(value / maxValue, 1f);

            this.value = value;
        }
    }
}
