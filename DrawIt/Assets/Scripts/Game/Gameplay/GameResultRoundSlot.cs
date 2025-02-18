﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameResultRoundSlot : MonoBehaviour
{
    [SerializeField] private RawImage resultImage;
    [SerializeField] private TMP_Text expectedDrawingTitle;
    [SerializeField] private TMP_Text aiGuessTitle;

    public void UpdateUi(GameplayRound round)
    {
        expectedDrawingTitle.text = $"Requested: \n{round.GetExpectedDrawingName}";
        aiGuessTitle.text = $"AI guess: \n{round.GetGuessData.GetResultItem}";
        resultImage.texture = round.GetResultTexture;
    }
}