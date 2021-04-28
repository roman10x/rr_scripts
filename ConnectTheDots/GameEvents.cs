using UnityEngine.Events;


// UnityEvent with integer argument
public class UnityIntEvent : UnityEvent<int> { }


// UnityEvent with two integer arguments, used to broadcast changes to the score
public class ScoreChangedEvent : UnityEvent<int, int> { }


// Static class holding all relevant game events
public static class GameEvents
{
    // Event that is fired when bubbles are despawned
    public static UnityIntEvent OnElementsDespawned { get; } = new UnityIntEvent();

    // Event that is fired when the selection of bubbles has changed
    public static UnityIntEvent OnSelectionChanged { get; } = new UnityIntEvent();

    // Event that is fired when the score has changed
    public static ScoreChangedEvent OnScoreChanged { get; } = new ScoreChangedEvent();
}