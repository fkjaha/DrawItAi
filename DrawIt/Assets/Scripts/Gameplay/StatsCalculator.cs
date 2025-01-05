using System.Collections.Generic;
using UnityEngine;

public class StatsCalculator : MonoBehaviour
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
        Debug.Log($"Engine: {engine.GetModelName} | {rightGuessCount/datasetTextures.Count} Accuracy Rate");
    }
    
    // double CalculatePrecision(int tp, int fp)
    // {
    //     return tp / (double)(tp + fp);
    // }
    //
    // double CalculateRecall(int tp, int fn)
    // {
    //     return tp / (double)(tp + fn);
    // }
    //
    // double CalculateF1(double precision, double recall)
    // {
    //     return 2 * (precision * recall) / (precision + recall);
    // }
}