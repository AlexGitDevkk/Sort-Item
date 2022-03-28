using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSkin : MonoBehaviour
{
    [SerializeField] private List<Renderer> _renderers = new List<Renderer>();

    public void ChangeColors(Color newColor)
    {
        for (int i = 0; i < _renderers.Count; i++)
        {
            _renderers[i].material.color = newColor;
        }
    }
}
