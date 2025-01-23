using UnityEngine;

public class RoundSounds : MonoBehaviour
{
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private SoundPresetBase roundCountdownSound;
    [SerializeField] private SoundPresetBase roundTimeSound;

    private int _lastSecondCountdownPlayed;
    private int _lastSecondRoundTimePlayed;

    private void Awake()
    {
        roundManager.OnCountdownRoundStartedEvent += UpdateCountdownTime;
        roundManager.OnRoundCountdownTimeUpdatedEvent  += PlayCountdownSoundIf1SecondPassed;
        
        roundManager.OnRoundStartedEvent += UpdateRoundTime;
        roundManager.OnRoundTimeUpdatedEvent  += PlayRoundTimeSoundIf1SecondPassed;
    }

    private void UpdateCountdownTime()
    {
        _lastSecondCountdownPlayed = Mathf.CeilToInt(roundManager.GetRoundCountdownTimeLeft);
    }

    private void PlayCountdownSoundIf1SecondPassed()
    {
        if (_lastSecondCountdownPlayed - roundManager.GetRoundCountdownTimeLeft >= 1)
        {
            UpdateCountdownTime();
            SoundsPlayer.Instance.PlaySound(roundCountdownSound);
        }
    }
    
    private void UpdateRoundTime()
    {
        _lastSecondRoundTimePlayed = Mathf.CeilToInt(roundManager.GetRoundTimeLeft);
    }

    private void PlayRoundTimeSoundIf1SecondPassed()
    {
        if (_lastSecondRoundTimePlayed - roundManager.GetRoundTimeLeft >= 1)
        {
            UpdateRoundTime();
            SoundsPlayer.Instance.PlaySound(roundTimeSound);
        }
    }
}