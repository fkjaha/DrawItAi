using TMPro;
using UnityEngine;

public class DrawTargetText : MonoBehaviour
{
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private TMP_Text drawTargetText;

    private void OnEnable()
    {
        roundManager.OnRoundDataUpdatedEvent += UpdateText;
        UpdateText();
    }

    private void OnDisable()
    {
        roundManager.OnRoundDataUpdatedEvent -= UpdateText;
    }

    private void UpdateText()
    {
        if (roundManager.GetActiveRound == null) return;
        drawTargetText.text = $"{roundManager.GetActiveRound.GetExpectedDrawingName}";
    }
}