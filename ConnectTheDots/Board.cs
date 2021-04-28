using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Board : MonoBehaviour 
{
    [SerializeField] private BoardElement tilePrefab;
    [SerializeField] private ConnectionLine connectionLine = null;
    [SerializeField] private int rowCount; 
    [SerializeField] private int columnCount;
    [SerializeField] private List<Color> colors = new List<Color>();

    
    private BoardInput boardInput;
    private BoardMovement boardMovement;
    private BoardSpawn boardSpawn;
    
    public List<BoardElement> Elements { get; } = new List<BoardElement>();
    public List<Color> Colors => colors;
    
    public Vector2 StartCellPosition => BoardCenter - .5f * new Vector2(ColumnCount - 1, this.RowCount - 1) / 2.0f;
    
    public Vector2 BoardCenter => new Vector2(0,0); // Zero point for this project only
    
    public int RowCount => rowCount;
    public int ColumnCount => columnCount;
    
    // Returns the Transform containing all grid elements 
    public Transform GridContainer => transform;

    private void Awake()
    {
        boardInput = new BoardInput(this, connectionLine);
        boardSpawn = new BoardSpawn(this);
        boardMovement = new BoardMovement(this);
    }
   
    // Creates all elements of the board and sets them up
    public void CreateBoard () 
    {
        for (int y = 0; y < ColumnCount; y++) 
        {
            for (int x = 0; x < RowCount; x++)
            {
                BoardElement element = Instantiate<BoardElement>(tilePrefab);
                
                element.transform.position = this.BoardToWorldPosition(x, y);
                
                element.transform.SetParent(this.GridContainer.transform, true);

                element.Color = colors[Random.Range(0, colors.Count)];

                Elements.Add(element);
            }
        }
    }

    public Coroutine WaitForMovement()
    {
        return StartCoroutine(boardMovement.WaitForMovement());
    }
    public Coroutine WaitForSelection()
    {
        return StartCoroutine(boardInput.WaitForSelection());
    }
    
    public Coroutine RespawnElements()
    {
        return StartCoroutine(boardSpawn.RespawnElements());
    }
    
    public Coroutine DespawnSelection()
    {
        return StartCoroutine(boardSpawn.Despawn(this.boardInput.SelectedElements));
    }

}

