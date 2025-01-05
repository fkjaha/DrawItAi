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
        DateTime startTime = DateTime.Now;
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
            
            // Debug.Log($"Engine: {engine.GetModelName} | {datasetElement.groupIndex} Group Index | {truePositives} TP | {trueNegatives} TN | {falsePositives} FP | {falseNegatives} FN");
        }

        float averagePrecision = precisions.Average();
        float averageRecall = recalls.Average();
        float f1 = CalculateF1(averagePrecision, averageRecall);
        int timeToCalculate = (DateTime.Now - startTime).Milliseconds;
        Debug.Log($"Engine: {engine.GetModelName} | {timeToCalculate} TTC | {f1} F1 | {averagePrecision} P | {averageRecall} R");
    }
    
    private float CalculatePrecision(int tp, int fp)
    {
        // Debug.Log($"Calculating Precision {tp}/{tp + fp}");
        float divider = tp + fp;
        if (divider == 0) return 0;
        return tp / divider;
    }
    
    private float CalculateRecall(int tp, int fn)
    {
        // Debug.Log($"Calculating Recall {tp}/{tp + fn}");
        float divider = tp + fn;
        if (divider == 0) return 0;
        return tp / divider;
    }
    
    private float CalculateF1(float precision, float recall)
    {
        // Debug.Log($"F1 {precision}/{recall}");
        float divider = precision + recall;
        if (divider == 0) return 0;
        return 2 * (precision * recall) / divider;
    }
}

[Serializable]
public class PositivesNegativesDatasetElement
{
    public int groupIndex;
    public List<Texture2D> positivesList;
    public List<Texture2D> negativesList;
}