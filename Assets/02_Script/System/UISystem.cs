using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;

public class UISystem : MonoBehaviour
{
    public static UISystem Instance;

    public event Action OnEnemyKillEvt;

    [SerializeField] private GameoverUI gameoverUI;
    [SerializeField] private EXGaugeBar gaugeBar;
    [SerializeField] private Profle profle;
    [SerializeField] private UX ux;
    [SerializeField] private WaveUI waveUI;

    public EXGaugeBar EXGaugeBar => gaugeBar;
    public Profle Profle => profle;
    public WaveUI WaveUI => waveUI;

    public float highSocre_time { get; private set; }
    public int highSocre_kill { get; private set; }

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
        if (Input.anyKeyDown && !ControlSystem.Instance.isGameStart)
        {
            ControlSystem.Instance.SetGameStart();
        }

        if (!ControlSystem.Instance.IsStopLogic())
        {
            highSocre_time += Time.deltaTime;
        }
    }

    public void GetKillCount()
    {
        highSocre_kill++;

        OnEnemyKillEvt?.Invoke();
    }

    private void HandleGameover()
    {
        gameoverUI.GameoverPanel(highSocre_time, highSocre_kill);
    }
}
