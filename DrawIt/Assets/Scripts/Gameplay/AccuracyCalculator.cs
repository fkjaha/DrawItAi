using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccuracyCalculator : MonoBehaviour
{
    [SerializeField] private List<DatasetTexture> datasetTextures;
    [SerializeField] private List<GuessEngine> enginesList;
    [SerializeField] private TMP_Text outputText;

    public void CalculateEnginesAccuracy()
    {
        if (outputText != null) outputText.text = "";

        foreach (GuessEngine guessEngine in enginesList)
        {
            GetEngineAccuracy(guessEngine);
        }
    }
    
    public void GetEngineAccuracy(GuessEngine engine)
    {
        int rightGuessCount = 0;
        DateTime startTime = DateTime.Now;
        foreach (DatasetTexture datasetTexture in datasetTextures)
        {
            GuessData guessData = engine.TakeGuess(datasetTexture.texture);
            if (guessData.GetTopProbabilityIndex == datasetTexture.rightAnswerIndex) rightGuessCount++;
        }
        int timeToCalculate = (DateTime.Now - startTime).Milliseconds;
        
        string resultText =
            $"Engine: {engine.GetModelName} | {(float) rightGuessCount / datasetTextures.Count} Accuracy Rate | {timeToCalculate} TTC"; 
        Debug.Log(resultText);
        if (outputText != null) outputText.text += $"\n \n{resultText}";
    }
}