using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager : SingletonBehaviour<HighscoreManager>
{
    private static readonly string key = "Highscore";
    
    public int Highscore
    {
        get
        {
            //Return the highscore from player prefs, if it exists. Zero otherwise
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : 0;
        }
        set
        {
            //Store the given value to the player prefs
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }
    }
}
