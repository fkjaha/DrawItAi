using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private int numberOfRounds;
    [SerializeField] private RoundManager roundManager;

    private bool _gameActive;
    
    public void StartGame()
    {
        roundManager.StartGame();
    }
}