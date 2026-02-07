using System;
using System.Collections;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField, Range(-0.5f, 0.3f)] private float _xPositionModifier = 0f;
    private Vector3 _initialPosition;

    void Awake()
    {
        _initialPosition = transform.position;
        _initialPosition.x -= _xPositionModifier;
    }

    void Update()
    {
        Vector3 newPosition = _initialPosition;
        newPosition.x += _xPositionModifier;
        transform.position = newPosition;
    }
}
