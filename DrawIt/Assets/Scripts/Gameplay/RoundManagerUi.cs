using System;
using TMPro;
using UnityEngine;

public class RoundManagerUi : MonoBehaviour
{
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private GameObject countdownUI;
    [SerializeField] private GameObject resultUi;
    [SerializeField] private TMP_Text roundCountdownText;
    [SerializeField] private TMP_Text roundTimeText;
    [SerializeField] private TMP_Text roundDrawTarget;

    private void Awake()
    {
        roundManager.OnCountdownRoundStartedEvent += ShowCountdownUI;
        roundManager.OnRoundStartedEvent += HideCountdownUI;
        roundManager.OnRoundFinishedEvent += ShowResultUi;
        roundManager.OnRoundCountdownTimeUpdatedEvent += UpdateRoundCountdownUI;
        roundManager.OnRoundTimeUpdatedEvent += UpdateRoundTimeUi;
    }

    private void ShowResultUi()
    {
        resultUi.SetActive(true);
    }
    
    private void ShowCountdownUI()
    {
        countdownUI.SetActive(true);
    }

    private void HideCountdownUI()
    {
        countdownUI.SetActive(false);
    }

    private void UpdateRoundCountdownUI()
    {
        roundCountdownText.text = $"{(int)roundManager.GetRoundCountdownTimeLeft}";
    }

    private void UpdateRoundTimeUi()
    {
        roundTimeText.text = $"{(int)roundManager.GetRoundTimeLeft}";
    }
}