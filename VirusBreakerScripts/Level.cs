using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private int breakableBlocks;
    

    // cached reference
    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }
  

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void CountDestroyedBlocks()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            StartCoroutine(WaitForSceneLoad());
        }
    }

    
    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(.1f);
        sceneLoader.LoadNextScene();
     
    }
}
