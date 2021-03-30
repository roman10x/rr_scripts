using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 _startPosition;
    private float _repeatWidth;
    
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        _repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // when background position Y will be smaller than calculated repeat width, resetting position.
        if (transform.position.x < _startPosition.x - _repeatWidth)
        {
            transform.position = _startPosition;
        }

    }
}
