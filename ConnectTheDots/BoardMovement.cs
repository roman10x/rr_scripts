using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Performing the animation of movement of elements in the game board
public class BoardMovement
{
    private Board board;
    
    public BoardMovement(Board board)
    {
        this.board = board;
    }
    
    /// <summary>
    /// Moves all elements downwards if there are empty cells below them and 
    /// waits for their movement to be finished
    /// </summary>
    public IEnumerator WaitForMovement()
    {
        
        MoveElements();

        while (IsMovementDone() == false)
        {
            yield return new WaitForSeconds(0.05f);
        }

        yield break;
    }
    
    private bool IsMovementDone()
    {
        foreach (BoardElement element in board.Elements)
        {
            if (element.IsSpawned && element.IsMoving)
            {
                return false;
            }
        }

        return true;
    }
    private void MoveElements()
    {
        
        //Run from bottom to top through all rows
        for (int y = 0; y < board.RowCount; y++)
        {
            for (int x = 0; x < board.ColumnCount; x++)
            {
                ProcessCell(x, y);
                
            }
        }
    }
    
    /// <summary>
    /// Checks for the cell in the given row and column, whether it is not spawned.
    /// If thats the case, the next spawned element above it, will move to its position.
    /// </summary>
    private void ProcessCell(int column, int row)
    {
        BoardElement element = board.GetElement(column, row);

        //Is the element not spawned? 
        //=> Cell is empty and elements above should move downwards
        if (element.IsSpawned == false)
        {
            //Move the next element above it down
            for (int i = row + 1; i < board.RowCount; i++)
            {
                BoardElement next = board.GetElement(column, i);

                if (next != null && next.IsSpawned && !next.IsMoving)
                {
                        
                    MoveElement(new Vector2Int(column, i), new Vector2Int(column, row));
                    return;
                }
            }
        }
    }
    
    public BoardElement GetElement(int column, int row)
    {
        return board.Elements[row * board.ColumnCount + column];
    }
    
    
    private void MoveElement(Vector2Int oldPos, Vector2Int newPos)
    {
        //Catch the elements of the two grid positions
        BoardElement element1 = board.GetElement(oldPos.x, oldPos.y);
        BoardElement element2 = board.GetElement(newPos.x, newPos.y);

        //Switch them in the grid
        board.Elements[board.BoardPositionToIndex(oldPos)] = element2;
        board.Elements[board.BoardPositionToIndex(newPos)] = element1;

        //Update world positions
        element2.transform.position = board.BoardToWorldPosition(oldPos);
        element1.Move(board.BoardToWorldPosition(newPos), 0.4f);
    }
}
