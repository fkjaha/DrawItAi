using UnityEngine;

public class AutoRectAnchorsPlacer : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    [ContextMenu("Auto Anchors")]
    public void AutoAnchors()
    {
        RectTransform parentRectTransform = rectTransform.parent.GetComponent<RectTransform>();
        if (parentRectTransform == null) return;
        
        float parentWidth = parentRectTransform.rect.width;
        float parentHeight = parentRectTransform.rect.height;
        
        Vector2 anchorMaxChange = Vector2.zero;
        Vector2 anchorMinChange = Vector2.zero;
        
        anchorMaxChange.x = rectTransform.offsetMax.x / parentWidth;
        anchorMaxChange.y = rectTransform.offsetMax.y / parentHeight;
        anchorMinChange.x = rectTransform.offsetMin.x / parentWidth;
        anchorMinChange.y = rectTransform.offsetMin.y / parentHeight;
        
        rectTransform.anchorMax += anchorMaxChange;
        rectTransform.anchorMin += anchorMinChange;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;
    }
}