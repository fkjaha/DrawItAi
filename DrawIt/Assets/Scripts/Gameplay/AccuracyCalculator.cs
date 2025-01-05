using System.Collections.Generic;
using UnityEngine;

public class AccuracyCalculator : MonoBehaviour
{
    [SerializeField] private List<DatasetTexture> datasetTextures;
    [SerializeField] private List<GuessEngine> enginesList;

    public void CalculateEnginesAccuracy()
    {
        foreach (GuessEngine guessEngine in enginesList)
        {
            GetEngineAccuracy(guessEngine);
        }
    }
    
    public void GetEngineAccuracy(GuessEngine engine)
    {
        int rightGuessCount = 0;
        foreach (DatasetTexture datasetTexture in datasetTextures)
        {
            GuessData guessData = engine.TakeGuess(datasetTexture.texture);
            if (guessData.GetTopProbabilityIndex == datasetTexture.rightAnswerIndex) rightGuessCount++;
        }
        Debug.Log($"Engine: {engine.GetModelName} | {(float)rightGuessCount/datasetTextures.Count} Accuracy Rate");
    }
}