using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberWizard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI testText;
    
    private int minNumber = 1;
    private int maxNumber = 1000;
    int testNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        GameStarting();        
    }

    void GameStarting()
    {
        NextTest();
    }
    
    // Updating test number and showing it to player
    void NextTest()
    {
        testNumber = Random.Range(minNumber, maxNumber + 1);
        testText.text = testNumber.ToString();
    }

    public void OnPressHigher()
    {
        minNumber = testNumber + 1;
        if (minNumber >= maxNumber)
        {
            minNumber = maxNumber;
        }
        NextTest();
    }
    
    public void OnPressLower()
    {
        maxNumber = testNumber - 1;
        NextTest();
    }
}
