using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirteenPixels.Soda;

public class Door : MonoBehaviour
{
    [SerializeField] GameEvent levelPassed;
    [SerializeField] List<GameObject> door;
    [SerializeField] List<GameObject> enableOnExit;
    private void OnEnable()
    {
        levelPassed.onRaise.AddResponse(OpenTheDoor);
    }
    private void OnDisable()
    {
        levelPassed.onRaise.RemoveResponse(OpenTheDoor);
    }

    
    // Opens the door and turning ON exit 
    private void OpenTheDoor()
    {
        foreach (var block in door)
        {
            block.SetActive(false);
        }
        foreach (var block in enableOnExit)
        {
            block.SetActive(true);
        }
    }
}
