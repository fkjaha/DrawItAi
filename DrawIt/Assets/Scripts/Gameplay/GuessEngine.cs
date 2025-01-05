using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.Sentis;
using UnityEngine;
using UnityEngine.UI;

public class GuessEngine : MonoBehaviour
{
    public Dictionary<int, string> GetAnswersDictionary => answersDictionary;
    public string GetModelName => onnxModel.name;
    
    [Header("Model")]
    [SerializeField] private ModelAsset onnxModel;
    [Header("Input")]
    [SerializeField] private RawImage inputImage;
    [SerializeField] private bool resizeImage;
    [SerializeField] private int imageResizeSize = 28;
    [Header("Output")]
    [SerializeField] private RawImage outputRawImage;
    [SerializeField] private TMP_Text outputText;

    [Header("Config")]
    [SerializeField] private BackendType backendType = BackendType.GPUCompute;
    [TextArea(0, 10)]
    [SerializeField] private string configJson;

    IWorker engine;
    TensorFloat inputTensor;
    Ops ops;

    private Dictionary<int, string> answersDictionary = new();

    private void Awake()
    {
        answersDictionary = ParseJsonToAnswersDictionary(configJson);
    }

    void Start()
    {
        Model model = ModelLoader.Load(onnxModel);
        engine = WorkerFactory.CreateWorker(backendType, model);
        ops = WorkerFactory.CreateOps(backendType, null);
    }

    public GuessData TakeGuess(Texture2D texture)
    {
        inputTensor?.Dispose();

        Texture2D inputTexture = GetProcessedInputTexture(texture);
        
        inputTensor = TextureConverter.ToTensor(inputTexture, imageResizeSize, imageResizeSize, 1);
        engine.Execute(inputTensor);
        
        TensorFloat result = engine.PeekOutput() as TensorFloat;
        
        TensorFloat probabilities = ops.Softmax(result);
        TensorInt indexOfMaxProbability = ops.ArgMax(probabilities, -1, false);
        
        probabilities.MakeReadable();
        indexOfMaxProbability.MakeReadable();

        int predictedThingIndex = indexOfMaxProbability[0];
        float probability = probabilities[predictedThingIndex];
        
        GuessData guessResultData = new GuessData(answersDictionary[predictedThingIndex], probabilities.ToReadOnlyArray(), predictedThingIndex);
        return guessResultData;
    }

    private Texture2D GetProcessedInputTexture(Texture2D initialTexture)
    {
        Texture2D resultTexture = initialTexture;
        if (resizeImage)
        {
            Texture2D resizedTexture = new(imageResizeSize, imageResizeSize);
            Graphics.ConvertTexture(initialTexture, resizedTexture);
            resultTexture = resizedTexture;
        }
        return resultTexture;
    }

    private Dictionary<int, string> ParseJsonToAnswersDictionary(string json)
    {
        Dictionary<int, string> dictionary = new();

        string[] pairs = json.Split(',');

        foreach (string pair in pairs)
        {
            string[] keyValue = pair.Split(':');

            string keyString = keyValue[0].Trim().Trim('\"');
            string valueString = keyValue[1].Trim().Trim('\"');
            
            int key = int.Parse(keyString);
            dictionary.Add(key, valueString);
        }

        return dictionary;
    }

    [ContextMenu("Test Raw Image")]
    public void TestRawImage()
    {
        inputTensor?.Dispose();

        Texture2D inputTexture = inputImage.texture as Texture2D;
        
        if (resizeImage)
        {
            Texture2D resizedTexture = new(imageResizeSize, imageResizeSize);
            Graphics.ConvertTexture(inputImage.texture, resizedTexture);
            inputTexture = resizedTexture;
        }
        outputRawImage.texture = inputTexture;
        
        // Convert the texture into a tensor, it has width=W, height=W, and channels=1:    
        inputTensor = TextureConverter.ToTensor(inputTexture, imageResizeSize, imageResizeSize, 1);
        
        // run the neural network:
        engine.Execute(inputTensor);
        
        // We get a reference to the output of the neural network while keeping it on the GPU
        TensorFloat result = engine.PeekOutput() as TensorFloat;
        
        TensorFloat probabilities = ops.Softmax(result);
        TensorInt indexOfMaxProbability = ops.ArgMax(probabilities, -1, false);
        
        probabilities.MakeReadable();
        indexOfMaxProbability.MakeReadable();

        int predictedThingIndex = indexOfMaxProbability[0];
        float probability = probabilities[predictedThingIndex];
        
        Debug.Log($"{predictedThingIndex} : {probability}");
        // result.MakeReadable();
        StringBuilder probabilitiesTable = new();
        foreach (float prob in probabilities.ToReadOnlyArray())
        {
            probabilitiesTable.Append($"{prob*100:F2}%\n");
        }
        Debug.Log($"{probabilitiesTable}");
        outputText.text = answersDictionary[predictedThingIndex];
    }

    private void OnDestroy()
    {
        inputTensor?.Dispose();
        engine?.Dispose();
        ops?.Dispose();
    }
}