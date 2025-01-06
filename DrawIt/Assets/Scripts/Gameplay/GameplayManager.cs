using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public event UnityAction OnGameStartedEvent;
    public event UnityAction OnGameFinishedEvent;
    public event UnityAction OnRoundUpdatedEvent;
    
    public List<GameplayRound> GetRounds => _rounds;
    public int GetNumberOfRounds => numberOfRounds;
    public int GetRoundNumber => _roundNumber;

    [SerializeField] private int numberOfRounds;
    [SerializeField] private RoundManager roundManager;

    private bool _gameActive;
    private bool _roundFinished;
    private bool _continuePressed;
    private List<GameplayRound> _rounds = new();
    private int _roundNumber;
    
    private void Awake()
    {
        roundManager.OnRoundFinishedEvent += OnRoundFinished;
    }

    public void StartGame()
    {
        if (_gameActive) return;
        _gameActive = true;
        OnGameStartedEvent?.Invoke();
        StartCoroutine(GameRoutine());
    }

    private IEnumerator GameRoutine()
    {
        _roundNumber = 0;
        OnRoundUpdatedEvent?.Invoke();
        while (_roundNumber < numberOfRounds)
        {
            _roundFinished = false;
            _continuePressed = false;
            roundManager.StartGame();
            yield return new WaitUntil(() => _roundFinished);
            yield return new WaitUntil(() => _continuePressed);
            _roundNumber++;
            OnRoundUpdatedEvent?.Invoke();
            _rounds.Add(roundManager.GetActiveRound);
        }
        _gameActive = false;
        OnGameFinishedEvent?.Invoke();
    }

    public void Continue()
    {
        _continuePressed = true;
    }

    private void OnRoundFinished()
    {
        _roundFinished = true;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}