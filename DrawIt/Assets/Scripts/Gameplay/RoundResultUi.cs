using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundResultUi : MonoBehaviour
{
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private RawImage resultImage;
    [SerializeField] private TMP_Text expectedName;

    private void OnEnable()
    {
        roundManager.OnRoundFinishedEvent += UpdateUi;
        UpdateUi();
    }

    private void OnDisable()
    {
        roundManager.OnRoundFinishedEvent -= UpdateUi;
    }

    private void UpdateUi()
    {
        GameplayRound round = roundManager.GetActiveRound;
        if (round == null) return;
        resultImage.texture = round.GetResultTexture;
        expectedName.text = round.GetExpectedDrawingName;
    }
}