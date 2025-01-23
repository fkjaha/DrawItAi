using System;
using UnityEngine;

[Serializable]
public class GameplayRound
{
    public int GetExpectedDrawingIndex => expectedDrawingIndex;
    public string GetExpectedDrawingName => expectedDrawingName;
    public Texture2D GetResultTexture => resultTexture;
    public GuessData GetGuessData => guessData;

    [SerializeField] private int expectedDrawingIndex;
    [SerializeField] private string expectedDrawingName;
    [SerializeField] private Texture2D resultTexture;
    [SerializeField] private GuessData guessData;
    
    public GameplayRound(int expectedDrawingIndex, string expectedDrawingName, int textureSize)
    {
        this.expectedDrawingIndex = expectedDrawingIndex;
        this.expectedDrawingName = expectedDrawingName;
        resultTexture = new Texture2D(textureSize, textureSize)
        {
            filterMode = FilterMode.Point
        };
    }

    public void SetResultTexture(Texture2D texture)
    {
        Graphics.ConvertTexture(texture, resultTexture);
    }

    public void SetGuessData(GuessData guessData)
    {
        this.guessData = guessData;
    }
}