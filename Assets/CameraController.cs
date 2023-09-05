using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        SmoothFollow();
        RotateAroundTarget();
    }

    private void SmoothFollow()
    {
        var desiredPosition = target.position + offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
    
    private void RotateAroundTarget()
    {
        transform.LookAt(target, Vector3.up);
    }
}
