using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameBoardExtensions 
{
    /// <summary>
    /// Gets the element of the board in the given column and row 
    /// </summary>
  
    public static BoardElement GetElement(this Board board, int column, int row)
    {
        return board.Elements[row * board.ColumnCount + column];
    }

    /// <summary>
    /// Gets the world position of the given position in the grid
    /// </summary>
   
    public static Vector2 BoardToWorldPosition(this Board board, int column, int row)
    {
        return board.StartCellPosition + .5f * new Vector2(column, row);
    }

    /// <summary>
    /// Gets the world position of the given position in the grid
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="gridPos"></param>
    /// <returns></returns>
    public static Vector2 BoardToWorldPosition(this Board board, Vector2Int gridPos)
    {
        return board.BoardToWorldPosition(gridPos.x, gridPos.y);
    }

    /// <summary>
    /// Gets the index of the element at the given grid position
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="gridPos"></param>
    /// <returns></returns>
    public static int BoardPositionToIndex(this Board board, Vector2Int gridPos)
    {
        return board.ColumnCount * gridPos.y + gridPos.x;
    }
    
    
}

