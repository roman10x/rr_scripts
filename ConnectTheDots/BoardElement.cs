using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BoardElement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    public bool IsMoving { get; private set; }
    public bool IsSpawned => gameObject.activeSelf;
    
    public Color Color
    {
        get { return spriteRenderer.color; }
        set { spriteRenderer.color = value; }
    }
    
    public void Move(Vector3 targetPos, float time)
    {
        IsMoving = true;

        StartCoroutine(MoveCoroutine(targetPos, time, () =>
        {
            IsMoving = false;
        }));
    }
    
    // Activates the element and plays its spawn animation
    public void Spawn()
    {
        gameObject.SetActive(true);

        Vector2 startScale = Vector2.one * 0.1f;
        Vector2 endScale = Vector2.one * .25f;

        StartCoroutine(ScaleCoroutine(startScale, endScale, 0.25f));
    }

    // Plays the despawn animation and deactivates the element afterwards
    public void Despawn()
    {
        Vector2 startScale = Vector2.one * .25f;
        Vector2 endScale = Vector2.one * 0.1f;
        

        StartCoroutine(ScaleCoroutine(startScale, endScale, 0.25f, () =>
        {
            gameObject.SetActive(false);
        }));
    }
    
    
    
    // A coroutine scaling the transform
    public IEnumerator ScaleCoroutine(Vector2 from, Vector2 to, float time, UnityAction onComplete = null)
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

            transform.localScale = Vector3.Lerp(from, to, elapsedTime / time);

            yield return null;
        }

        transform.localScale = to;

        onComplete?.Invoke();

        yield break;
    }
    
    public IEnumerator MoveCoroutine(Vector2 targetPos, float time, UnityAction onComplete = null)
    {
        Vector2 startPos = transform.position;

        float currentTime = Time.timeSinceLevelLoad;
        float elapsedTime = 0.0f;
        float lastTime = currentTime;

        while (time>0 && elapsedTime < time)
        {
            //Update Time
            currentTime = Time.timeSinceLevelLoad;
            elapsedTime += currentTime - lastTime;
            lastTime = currentTime;

            transform.position = Vector2.Lerp(startPos, targetPos, elapsedTime / time);

            yield return null;
        }

        transform.position = targetPos;

        onComplete?.Invoke();

        yield break;
    }
}
