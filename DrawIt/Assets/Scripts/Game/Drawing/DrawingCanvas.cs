using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DrawingCanvas : MonoBehaviour
{
    public event UnityAction OnDrawingStartedEvent;
    public event UnityAction OnDrawingFinishedEvent;
    
    public Texture2D GetTexture => _texture;
    public int GetCanvasSize => canvasSize;

    [SerializeField] private RawImage rawImage;
    [SerializeField] private Texture2D inputTexture;
    [SerializeField] private int canvasSize;
    [SerializeField] private Color drawColor = Color.black;
    [SerializeField] private float brushSize = 5f;

    private Texture2D _texture;
    private Vector2 _previousPosition;
    private int _canvasWidth;
    private int _canvasHeight;
    private bool _isDrawing;
    
    private void Start()
    {
        _canvasHeight = canvasSize;
        _canvasWidth = canvasSize;
        InitializeTexture();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rawImage.rectTransform, Input.mousePosition, null, out Vector2 localPoint))
            {
                Vector2 textureCoord = LocalToTextureCoordinates(localPoint);
                DrawAtPosition(textureCoord);
                SetIsDrawing(true);
                return;
            }
            SetIsDrawing(false);
        }
        SetIsDrawing(false);
    }

    private void SetIsDrawing(bool newValue)
    {
        bool previousValue = _isDrawing;
        _isDrawing = newValue;
        if (newValue != previousValue)
        {
            if (_isDrawing) OnDrawingStartedEvent?.Invoke();
            else OnDrawingFinishedEvent?.Invoke();
        }
    }

    private void InitializeTexture()
    {
        _texture = inputTexture;
        _texture.Reinitialize(_canvasHeight, _canvasWidth);
        ClearCanvas();
        rawImage.texture = _texture;
    }

    private Vector2 LocalToTextureCoordinates(Vector2 localPoint)
    {
        return new Vector2(
            (localPoint.x + rawImage.rectTransform.rect.width / 2) / rawImage.rectTransform.rect.width * _canvasWidth,
            (localPoint.y + rawImage.rectTransform.rect.height / 2) / rawImage.rectTransform.rect.height * _canvasHeight
        );
    }

    private void DrawAtPosition(Vector2 position)
    {
        if (position.x < 0 || position.x > _canvasWidth - 1) return;
        if (position.y < 0 || position.y > _canvasWidth - 1) return;
        int x = Mathf.Clamp((int)position.x, 0, _canvasWidth - 1);
        int y = Mathf.Clamp((int)position.y, 0, _canvasHeight - 1);

        for (int i = -Mathf.CeilToInt(brushSize); i <= Mathf.CeilToInt(brushSize); i++)
        {
            for (int j = -Mathf.CeilToInt(brushSize); j <= Mathf.CeilToInt(brushSize); j++)
            {
                float distance = Mathf.Sqrt(i * i + j * j);
                if (distance <= brushSize)
                {
                    int pixelX = x + i;//Mathf.Clamp(x + i, 0, canvasWidth - 1);
                    int pixelY = y + j;//Mathf.Clamp(y + j, 0, canvasHeight - 1);
                    if (pixelX < 0 || pixelX > _canvasWidth - 1) continue;
                    if (pixelY < 0 || pixelX > _canvasWidth - 1) continue;
                    _texture.SetPixel(pixelX, pixelY, drawColor);
                }
            }
        }

        _texture.Apply();
    }

    public void ClearCanvas()
    {
        Color[] clearPixels = new Color[_canvasWidth * _canvasHeight];
        for (int i = 0; i < clearPixels.Length; i++)
        {
            clearPixels[i] = Color.black;
        }
        _texture.SetPixels(clearPixels);
        _texture.Apply();
    }

    public void SetColorBlack()
    {
        drawColor = Color.black;
    }
    
    public void SetColorWhite()
    {
        drawColor = Color.white;
    }
}