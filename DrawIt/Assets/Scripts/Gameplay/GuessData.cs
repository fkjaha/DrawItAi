using System;
using UnityEngine;

[Serializable]
public class GuessData
{
    public int GetTopProbabilityIndex => topProbabilityIndex;
    public float[] GetProbabilities => probabilities;
    public string GetResultItem => resultItem;
    
    [SerializeField] private int topProbabilityIndex;
    [SerializeField] private float[] probabilities;
    [SerializeField] private string resultItem;

    public GuessData(string resultItem, float[] probabilities, int topProbabilityIndex)
    {
        this.resultItem = resultItem;
        this.probabilities = probabilities;
        this.topProbabilityIndex = topProbabilityIndex;
    }

    public override string ToString()
    {
        return $"{resultItem} with {probabilities[topProbabilityIndex] * 100:F2}%";
    }
}