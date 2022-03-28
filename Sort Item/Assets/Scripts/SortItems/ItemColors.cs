using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColors : MonoBehaviour
{
    [Header("For Index")]
    [SerializeField] private int _rowIndex;
    [Header("Choose Skin")]
    [SerializeField] private int _skinIndex;
    [Header("Do color by: ")]
    [SerializeField] private Gradient _gradient;
    [SerializeField] private List<GameObject> _items = new List<GameObject>();

    private void Start()
    {
        InitializeItems();
    }

    private void InitializeItems()
    {
        float colorPercent = 0f;
        for (int i = 0; i < _items.Count; i++)
        {
            colorPercent = (float)i / (float)_items.Count;
            var _item = _items[i].GetComponent<ItemController>();

            _item.RowIndex = _rowIndex;
            _item.ColumnIndex = i;

            for (int j = 0; j < _item.Skins.Count; j++)
            {
                if (j == _skinIndex)
                    _item.Skins[j].gameObject.SetActive(true);
                else
                    _item.Skins[j].gameObject.SetActive(false);
            }

            _item.Skins[_skinIndex].GetComponent<ItemSkin>().ChangeColors(_gradient.Evaluate(colorPercent));
        }
    }
}
