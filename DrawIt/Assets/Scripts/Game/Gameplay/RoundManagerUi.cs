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
        roundManager.OnRoundStartedEvent += ShowRoundUi;
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
        resultUi.SetActive(false);
    }

    private void ShowRoundUi()
    {
        Debug.Log("Round started");
        countdownUI.SetActive(false);
        resultUi.SetActive(false);
    }

    private void UpdateRoundCountdownUI()
    {
        roundCountdownText.text = $"{(int)roundManager.GetRoundCountdownTimeLeft + 1}";
    }

    private void UpdateRoundTimeUi()
    {
        roundTimeText.text = $"{(int)roundManager.GetRoundTimeLeft + 1}";
    }
}