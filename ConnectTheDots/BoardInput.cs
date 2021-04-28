using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardInput
{
    private Board board;
    private ConnectionLine connectionLine;
    
    public List<BoardElement> SelectedElements { get; private set; }
    public BoardInput(Board board, ConnectionLine connectionLine)
    {
        this.board = board;
        this.connectionLine = connectionLine;
        SelectedElements = new List<BoardElement>(); ;
    }
    
    // Clear current selection
    private void Clear()
    {
        connectionLine.Clear();
        SelectedElements.Clear();

        GameEvents.OnSelectionChanged.Invoke(SelectedElements.Count);
    }
    
    // Processes the user input and waits until a selection has been made
    public IEnumerator WaitForSelection()
    {
        Clear();

        while (true)
        {
            yield return null;

            //Does the user touch the screen?
            if (Input.GetMouseButton(0))
            {
                //Does the user touch an element?
                if (InputRaycast(out BoardElement element))
                {
                    ProcessBoardElement(element);
                }
            }
            //Selection finished?
            else if (SelectedElements.Count > 0)
            {
                if (IsValidInput())
                {
                    //Clear selection line when finished
                    connectionLine.Clear();

                    //Stop coroutine
                    yield break;
                }
                else
                {
                    Clear();
                }
            }
        }
    }
    
    /// <summary>
    /// Returns true if an element is touched by the mouse, false otherwise.
    /// Outs the touched element, null otherwise
    /// </summary>
    private bool InputRaycast(out BoardElement element)
    {
        element = null;

        //Get mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Shoot a raycast on z-level
        RaycastHit2D hitInfo = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hitInfo)
        {
            //Try to get the BoardElement element
            element = hitInfo.transform.GetComponent<BoardElement>();

            //Return true if the hit object is GameGridElement
            if (element != null)
            {
                return true;
            }
        }

        //Return false if nothing is hit or if the object is not BoardElement
        return false;
    }
    
    // Processes the input on the given element
    private void ProcessBoardElement(BoardElement element)
    {
        //No element selected yet? Select element!
        if (SelectedElements.Count == 0)
        {
            //Cache selected color
            connectionLine.Color = element.Color;

            //Add element to selection
            AddElementToSelection(element);
        }
        else
        {
            //Is the element already selected?
            if (SelectedElements.Contains(element))
            {
                //Did the player moved back? Deselect element!
                if (IsSecondLast(element))
                {
                    DeselectLast();
                }
            }
            //Not selected, correct color and in distance? Select element!
            else if (IsSelectable(element))
            {
                AddElementToSelection(element);
            }
        }
    }
    
    private bool IsSecondLast(BoardElement element)
    {
        return SelectedElements.Count >= 2 && 
               element.Equals(SelectedElements[SelectedElements.Count - 2]) == true;
    }
    
    private bool IsSelectable(BoardElement element)
    {
        return HasValidColor(element) && IsInDistance(element);
    }
    
    /// <summary>
    /// Returns true if the given element is adjacent to the last element in the selection,
    /// false otherwise
    /// </summary>
    private bool IsInDistance(BoardElement element)
    {
        Vector2 lastElementPos = SelectedElements.Last().transform.position;

        return Vector2.Distance(element.transform.position, lastElementPos) < .6f;
    }
    
    private bool HasValidColor(BoardElement element)
    {
        return element.Color == connectionLine.Color;
    }
    
    private bool IsValidInput()
    {
        return (SelectedElements.Count > 1) ? true : false;
    }
    
    private void AddElementToSelection(BoardElement element)
    {
        if (SelectedElements.Count > 0)
        {
            float pitch = 1.0f + SelectedElements.Count * 0.1f;
        }

        SelectedElements.Add(element);

        connectionLine.Color = element.Color;

        connectionLine.SetPositions(SelectedElements);

        GameEvents.OnSelectionChanged.Invoke(SelectedElements.Count);
    }
    
    private void DeselectLast()
    {
        SelectedElements.Remove(SelectedElements.Last());

        connectionLine.SetPositions(SelectedElements);

        GameEvents.OnSelectionChanged.Invoke(SelectedElements.Count);
    }
    
}
