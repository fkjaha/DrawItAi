using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrecisionRecallF1Calculator : MonoBehaviour
{
    [SerializeField] private List<PositivesNegativesDatasetElement> datasetElements;
    [SerializeField] private List<GuessEngine> enginesList;
    
    public void CalculateEnginesStats()
    {
        foreach (GuessEngine guessEngine in enginesList)
        {
            GetEngineStats(guessEngine);
        }
    }
    
    public void GetEngineStats(GuessEngine engine)
    {
        List<float> precisions = new();
        List<float> recalls = new();
        foreach (PositivesNegativesDatasetElement datasetElement in datasetElements)
        {
            int truePositives = 0;
            int falsePositives = 0;
            int trueNegatives = 0;
            int falseNegatives = 0;
            
            foreach (Texture2D positiveTexture in datasetElement.positivesList)
            {
                GuessData guessData = engine.TakeGuess(positiveTexture);
                if (guessData.GetTopProbabilityIndex == datasetElement.groupIndex) truePositives++;
                else falseNegatives++;
            }
            foreach (Texture2D negativeTexture in datasetElement.negativesList)
            {
                GuessData guessData = engine.TakeGuess(negativeTexture);
                if (guessData.GetTopProbabilityIndex == datasetElement.groupIndex) falsePositives++;
                else trueNegatives++;
            }
            
            precisions.Add(CalculatePrecision(truePositives, falsePositives));
            recalls.Add(CalculateRecall(truePositives, falseNegatives));
        }

        float averagePrecision = precisions.Average();
        float averageRecall = recalls.Average();
        float f1 = CalculateF1(averagePrecision, averageRecall);
        Debug.Log($"Engine: {engine.GetModelName} | {f1} F1 | {averagePrecision} P | {averageRecall} R");
    }
    
    private float CalculatePrecision(int tp, int fp)
    {
        return tp / (float)(tp + fp);
    }
    
    private float CalculateRecall(int tp, int fn)
    {
        return tp / (float)(tp + fn);
    }
    
    private float CalculateF1(float precision, float recall)
    {
        return 2 * (precision * recall) / (precision + recall);
    }
}

[Serializable]
public class PositivesNegativesDatasetElement
{
    public int groupIndex;
    public List<Texture2D> positivesList;
    public List<Texture2D> negativesList;
}