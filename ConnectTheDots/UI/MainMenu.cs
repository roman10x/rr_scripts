using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HighscoreText = null;

    private void Start()
    {
        HighscoreText.text = HighscoreManager.Instance.Highscore.ToString();
    }
}
