using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ConnectionLine : MonoBehaviour
{
    private LineRenderer lineRenderer = null;
    private Color color;
    
    
    // Gets or sets the color of the line
    public Color Color
    {
        get { return color; }
        set
        {
            color = value;
            lineRenderer.startColor = value;
            lineRenderer.endColor = value;
        }
    }
    
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    
    // Updates the line for the given list of selected elements
    public void SetPositions(List<BoardElement> selectedElements)
    {
        lineRenderer.positionCount = selectedElements.Count;

        for (int i = 0; i < selectedElements.Count; i++)
        {
            lineRenderer.SetPosition(i, selectedElements[i].transform.position);
        }
    }

    
    // Clears the line
    public void Clear()
    {
        lineRenderer.positionCount = 0;
    }
}
