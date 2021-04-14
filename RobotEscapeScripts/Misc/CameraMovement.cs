using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GlobalTransform player;
    [SerializeField] float cameraCatchUpDistance = 2f;
    [SerializeField] float cameraSpeed = 0.01f;
    
    private float cameraMinZ = -6.27f;
    private float cameraMaxZ = -4.35f;
    

    Vector3? cameraDestination;

    private void Update() 
    {
        if (Mathf.Abs(player.componentCache.position.z - transform.position.z) > cameraCatchUpDistance)
            cameraDestination = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(
                player.componentCache.position.z, cameraMinZ, cameraMaxZ));
        if (cameraDestination != null && Mathf.Abs(player.componentCache.position.z - transform.position.z) 
            < cameraCatchUpDistance/2)
            cameraDestination = null;
        if (cameraDestination != null)
            transform.position = Vector3.Lerp(transform.position, cameraDestination.Value, cameraSpeed);
    }
}
