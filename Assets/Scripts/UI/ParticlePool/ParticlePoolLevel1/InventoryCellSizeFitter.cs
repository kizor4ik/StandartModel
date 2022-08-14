using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCellSizeFitter : MonoBehaviour
{
    private RectTransform _rect;
    private GridLayoutGroup _gridLayout;
    [SerializeField] private float _preferredCellSize;

    void Start()
    {
        _rect = this.GetComponent<RectTransform>();
        _gridLayout = this.GetComponent<GridLayoutGroup>();
        float rectArea = _rect.rect.width * _rect.rect.height;
        float childCount = transform.childCount;
        float cellSize = Mathf.Min(Mathf.Sqrt(rectArea / childCount), _preferredCellSize);
        _gridLayout.cellSize = new Vector2(cellSize, cellSize);
    }


}
