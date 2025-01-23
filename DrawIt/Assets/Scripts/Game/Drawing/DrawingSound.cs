using UnityEngine;

public class DrawingSound : MonoBehaviour
{
    [SerializeField] private DrawingCanvas drawingCanvas;
    [SerializeField] private AudioSource drawingSound;

    private void Awake()
    {
        drawingCanvas.OnDrawingStartedEvent += PlayDrawingSound;
        drawingCanvas.OnDrawingFinishedEvent += StopDrawingSound;
    }

    private void PlayDrawingSound()
    {
        drawingSound.Play();
    }
    
    private void StopDrawingSound()
    {
        drawingSound.Stop();
    }
}