using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour
{
    public event UnityAction OnRoundDataUpdatedEvent;
    public event UnityAction OnCountdownRoundStartedEvent;
    public event UnityAction OnRoundStartedEvent;
    public event UnityAction OnRoundFinishedEvent;
    public event UnityAction OnRoundTimeUpdatedEvent;
    public event UnityAction OnRoundCountdownTimeUpdatedEvent;
    
    public float GetRoundTimeLeft => _roundTimeLeft;
    public float GetRoundCountdownTimeLeft => _roundCountdownTimeLeft;

    public GameplayRound GetActiveRound => _activeRound;

    [SerializeField] private GuessEngine guessEngine;
    [SerializeField] private DrawingCanvas drawingCanvas;
    [SerializeField] private float roundCountdownTime;
    [SerializeField] private float roundTime;

    private float _roundCountdownTimeLeft;
    private float _roundTimeLeft;
    private bool _roundActive;
    private GameplayRound _activeRound;

    private void Awake()
    {
        _activeRound = new(default, default, 1);
    }

    public void StartGame()
    {
        StopAllCoroutines();
        StartCoroutine(RoundRoutine());
    }

    private IEnumerator RoundRoutine()
    {
        CreateNewRoundData();
        
        _roundTimeLeft = roundTime;
        _roundCountdownTimeLeft = roundCountdownTime;
        OnCountdownRoundStartedEvent?.Invoke();
        drawingCanvas.ClearCanvas();
        
        OnRoundCountdownTimeUpdatedEvent?.Invoke();
        
        yield return StartCoroutine(WaitRoundCountdownRoutine());
        
        _roundActive = true;
        OnRoundStartedEvent?.Invoke();
        drawingCanvas.ClearCanvas();
        // start round

        OnRoundTimeUpdatedEvent?.Invoke();
        while (_roundTimeLeft > 0)
        {
            yield return null;
            _roundTimeLeft -= Time.deltaTime;
            OnRoundTimeUpdatedEvent?.Invoke();
        }

        GuessData guess = guessEngine.TakeGuess(drawingCanvas.GetTexture);
        _activeRound.SetResultTexture(drawingCanvas.GetTexture);
        _activeRound.SetGuessData(guess);
        OnRoundDataUpdatedEvent?.Invoke();
        
        //finish round
        _roundActive = false;
        OnRoundFinishedEvent?.Invoke();
    }

    private IEnumerator WaitRoundCountdownRoutine()
    {
        while (_roundCountdownTimeLeft > 0)
        {
            yield return null;
            _roundCountdownTimeLeft -= Time.deltaTime;
            OnRoundCountdownTimeUpdatedEvent?.Invoke();
        }
    }

    private void CreateNewRoundData()
    {
        Dictionary<int, string> possibleAnswers = guessEngine.GetAnswersDictionary;
        int randomIndex = Random.Range(0, possibleAnswers.Count);
        string answerName = possibleAnswers[randomIndex];
        _activeRound = new(randomIndex, answerName, drawingCanvas.GetCanvasSize);
        OnRoundDataUpdatedEvent?.Invoke();
    }
}