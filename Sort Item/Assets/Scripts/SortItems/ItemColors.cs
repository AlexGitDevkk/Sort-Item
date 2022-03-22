using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColors : MonoBehaviour
{
    [Header("For Index")]
    [SerializeField] private int _rowIndex;
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
            _items[i].GetComponent<ItemController>().GetComponent<ItemController>().RowIndex = _rowIndex;
            _items[i].GetComponent<ItemController>().GetComponent<ItemController>().ColumnIndex = i;
            _items[i].transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material.color = _gradient.Evaluate(colorPercent);
            _items[i].transform.GetChild(0).GetChild(3).GetComponent<Renderer>().material.color = _gradient.Evaluate(colorPercent);
        }       
    }
}
