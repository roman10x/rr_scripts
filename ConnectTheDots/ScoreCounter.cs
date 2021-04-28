using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    /// <summary>
    /// Listen to score changed events
    /// </summary>
    private void OnEnable()
    {
        GameEvents.OnScoreChanged.AddListener(OnScoreChanged);
    }

    /// <summary>
    /// Stop listening to score changed events
    /// </summary>
    private void OnDisable()
    {
        GameEvents.OnScoreChanged.RemoveListener(OnScoreChanged);
    }

    /// <summary>
    /// Gets called when the score has been changed
    /// </summary>
    private void OnScoreChanged(int oldScore, int newScore)
    {
        StopAllCoroutines();
        StartCoroutine(UpdateTextCoroutine(oldScore, newScore, 1.0f));
    }

    /// <summary>
    /// Smoothly updates the score text component over the given time
    /// </summary>
   
    private IEnumerator UpdateTextCoroutine(int from, int to, float time)
    {
        float currentTime = Time.timeSinceLevelLoad;
        float elapsedTime = 0.0f;
        float lastTime = currentTime;

        while (time > 0 && elapsedTime < time)
        {
            //Update Time
            currentTime = Time.timeSinceLevelLoad;
            elapsedTime += currentTime - lastTime;
            lastTime = currentTime;

            //Update text component with the interpolated value for the score
            float value = Mathf.Lerp(from, to, elapsedTime / time);
            scoreText.text = ((int)value).ToString();

            yield return null;
        }

        scoreText.text = to.ToString();
    }
}
