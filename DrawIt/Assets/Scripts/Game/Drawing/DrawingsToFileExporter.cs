using System.IO;
using UnityEditor;
using UnityEngine;

public class DrawingsToFileExporter : MonoBehaviour
{
    [SerializeField] private DrawingCanvas drawingCanvas;
    [SerializeField] private string path;

    private string _saveKey = "lastSaveImageIndex";
    private int _lastImageIndex;
    
    public void SaveCurrentTextureToFile()
    {
        _lastImageIndex = PlayerPrefs.GetInt(_saveKey, 0);
        
        string localPath = $"{path}/img_{_lastImageIndex}.png";
        byte[] bytes = drawingCanvas.GetTexture.EncodeToPNG();
        
        File.WriteAllBytes(localPath, bytes);
        Debug.Log("Saved to file: " + localPath);
        #if UNITY_EDITOR
        AssetDatabase.Refresh();
        #endif
        _lastImageIndex++;
        PlayerPrefs.SetInt(_saveKey, _lastImageIndex);
    }
}