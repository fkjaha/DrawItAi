using System.Collections.Generic;
using UnityEngine;

public class MainGameplayUi : MonoBehaviour
{
    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private GameObject gameResultsPanel;
    [SerializeField] private SlotsProvider<GameResultRoundSlot> resultSlotsProvider;

    private void Awake()
    {
        gameplayManager.OnGameFinishedEvent += ShowResultsUi;
    }

    private void ShowResultsUi()
    {
        List<GameplayRound> rounds = gameplayManager.GetRounds;
        List<GameResultRoundSlot> slots = resultSlotsProvider.RequestSlots(rounds.Count);
        for (int i = 0; i < rounds.Count; i++)
        {
            slots[i].UpdateUi(rounds[i]);
        }

        gameResultsPanel.SetActive(true);
    }
}