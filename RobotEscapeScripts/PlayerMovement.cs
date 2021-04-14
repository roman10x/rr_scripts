using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirteenPixels.Soda;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GlobalVector2 input;
    private Vector3 direction;
    Entity thisBody;
    Rigidbody playerRigidbody;

    private void OnEnable()
    {
        input.onChange.AddResponse(Move);
    }

    private void OnDisable()
    {
        input.onChange.RemoveResponse(Move);
    }

    private void Awake()
    {
        if(playerRigidbody==null)
            playerRigidbody = GetComponent<Rigidbody>();
        if(thisBody==null)
            thisBody = GetComponent<Entity>();
    }

    
    // Called when Input Vector2 is changed
    private void Move(Vector2 input)
    {
        float temp = Mathf.Max(Mathf.Abs(input.x), Mathf.Abs(input.y));
        input.Normalize();
        input *= temp;
        direction = new Vector3(input.x, 0, input.y);
    }

    private void FixedUpdate()
    {
        transform.LookAt(transform.position + direction);
#if UNITY_EDITOR
        Debug.DrawRay(transform.position, direction * 3, Color.red);
#endif
        playerRigidbody.velocity = direction * thisBody.Speed;
        if (direction == Vector3.zero)
            playerRigidbody.angularVelocity = direction;
    }
}
