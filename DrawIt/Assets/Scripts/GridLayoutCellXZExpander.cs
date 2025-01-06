using System;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutCellXZExpander : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup gridLayout;
    [SerializeField] private int amountPerRow = 3;
    [Header("Height")]
    [SerializeField] private bool changeCellHeightToFitAllInside;
    [SerializeField] private int rowsCount;

    private void Start()
    {
        RecalculateGridLayout();
    }

    private void OnEnable()
    {
        RecalculateGridLayout();
    }

    private void OnValidate()
    {
        RecalculateGridLayout();
    }

    [ContextMenu("Recalculate")]
    private void RecalculateGridLayout()
    {
        if (gridLayout == null) return;
        if (amountPerRow <= 0) return;
        if (gridLayout.cellSize.y <= 0) return;
        if (gridLayout.cellSize.x <= 0) return;

        RectTransform gridRect = gridLayout.GetComponent<RectTransform>();
        Vector2 finalCellSize = gridLayout.cellSize;
        
        float gridWidth = gridRect.rect.width;
        float widthForCells = gridWidth - gridLayout.padding.horizontal - gridLayout.spacing.x * (amountPerRow - 1);
        float calculatedCellWidth = widthForCells / amountPerRow;

        float expectedToActualWidthRatio = calculatedCellWidth / gridLayout.cellSize.x;

        finalCellSize *= expectedToActualWidthRatio;
        
        if (changeCellHeightToFitAllInside && rowsCount > 0)
        {
            float gridHeight = gridRect.rect.height;
            float heightForCells = gridHeight - gridLayout.padding.vertical - gridLayout.spacing.y * (rowsCount - 1);
            float calculatedCellHeight = heightForCells / rowsCount;

            finalCellSize.y = calculatedCellHeight;
        }
        
        gridLayout.cellSize = finalCellSize;
    }

    private void OnRectTransformDimensionsChange()
    {
        RecalculateGridLayout();
    }
}