using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MainGameplayUi : MonoBehaviour
{
    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private GameObject gameResultsPanel;
    [SerializeField] private SlotsProvider<GameResultRoundSlot> resultSlotsProvider;
    [SerializeField] private TMP_Text gameResultText;
    [SerializeField] private TMP_Text roundIndexText;

    private void Awake()
    {
        gameplayManager.OnGameFinishedEvent += ShowResultsUi;
        gameplayManager.OnRoundUpdatedEvent += UpdateRoundIndexText;
    }

    private void UpdateRoundIndexText()
    {
        roundIndexText.text = $"Round {gameplayManager.GetRoundNumber + 1}/{gameplayManager.GetNumberOfRounds}";
    }

    private void ShowResultsUi()
    {
        List<GameplayRound> rounds = gameplayManager.GetRounds;
        List<GameResultRoundSlot> slots = resultSlotsProvider.RequestSlots(rounds.Count);
        for (int i = 0; i < rounds.Count; i++)
        {
            slots[i].UpdateUi(rounds[i]);
        }

        int positiveRounds = rounds.Count(r => r.GetExpectedDrawingIndex == r.GetGuessData.GetTopProbabilityIndex);
        gameResultText.text = $"{positiveRounds} / {rounds.Count} Rounds";

        gameResultsPanel.SetActive(true);
    }
}