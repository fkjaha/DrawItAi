using UnityEditor;
using UnityEngine;

namespace MoreEditorShortcuts
{
    public class UiShortcuts
    {
        [MenuItem("EditorUtils/Ui/Set Anchors to rect corners  &s")]
        public static void TogglePlaymode()
        {
            RectTransform rectTransform = Selection.activeGameObject.GetComponent<RectTransform>();
            if (rectTransform == null) return;
            
            RectTransform parentRectTransform = rectTransform.parent.GetComponent<RectTransform>();
            if (parentRectTransform == null) return;
        
            Undo.RecordObject(rectTransform, "Set Anchors to rect corners");
            
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
}