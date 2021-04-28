using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Despawns and respawns elements in the game board
public class BoardSpawn
{
    private Board board;
    public BoardSpawn(Board board)
    {
        this.board = board;
    }
    
    // Respawns all despawned elements
    public IEnumerator RespawnElements()
    {
       
        foreach (BoardElement element in this.board.Elements)
        {
            if (element.IsSpawned == false)
            {
                element.Color = this.board.Colors[Random.Range(0, this.board.Colors.Count)];

                element.Spawn();
                
            }
        }

        yield return new WaitForSeconds(0.4f);
    }
    
    // Despawns the given list of elements
    public IEnumerator Despawn(List<BoardElement> elements)
    {
        
        foreach (BoardElement boardElement in elements)
        {
            boardElement.Despawn();
        }

        yield return new WaitForSeconds(0.4f);

        GameManager.Instance.MovesAvailable--;

        GameEvents.OnElementsDespawned.Invoke(elements.Count);
    }
}
