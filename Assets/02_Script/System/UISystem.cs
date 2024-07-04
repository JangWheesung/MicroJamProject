using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UISystem : MonoBehaviour
{
    public static UISystem Instance;

    [SerializeField] private GameoverUI gameoverUI;
    [SerializeField] private EXGaugeBar gaugeBar;
    [SerializeField] private Profle profle;
    [SerializeField] private UX ux;

    public EXGaugeBar EXGaugeBar => gaugeBar;
    public Profle Profle => profle;

    private float highSocre_time = 0f;
    private int highSocre_kill = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ControlSystem.Instance.OnDeathEvt += HandleGameover;

        ux.PopUXUI();
    }

    private void Update()
    {
        highSocre_time += Time.deltaTime;
    }

    public void GetKillCount()
    {
        highSocre_kill++;
    }

    private void HandleGameover()
    {
        gameoverUI.GameoverPanel(highSocre_time, highSocre_kill);
    }
}
