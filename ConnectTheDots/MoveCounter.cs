using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(TextMeshProUGUI))]
public class MoveCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;

    

    private void Start()
    {
        UpdateText();
    }

    /// <summary>
    /// Start listening to despawn events
    /// </summary>
    private void OnEnable()
    {
        GameEvents.OnElementsDespawned.AddListener(OnElementsDespawned);
    }

    /// <summary>
    /// Stop listening to despawn events
    /// </summary>
    private void OnDisable()
    {
        GameEvents.OnElementsDespawned.RemoveListener(OnElementsDespawned);
    }

    /// <summary>
    /// Callback which is invoked when bubbles are despawned
    /// </summary>
    /// <param name="count"></param>
    private void OnElementsDespawned(int count)
    {
        UpdateText();
    }

    /// <summary>
    /// Updated the UI text component showing the number of moves left
    /// </summary>
    private void UpdateText()
    {
        counterText.text = GameManager.Instance.MovesAvailable.ToString();
    }
    
}
