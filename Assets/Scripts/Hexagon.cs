using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public enum HexagonType { Normal }

[System.Serializable]
public struct HexagonData
{
    public HexagonType hexagonType;
    public Color hexagonColor;
}

public class Hexagon : MonoBehaviour, IPointerDownHandler
{
    public HexagonData hexagonData;

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
