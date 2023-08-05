using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;
// ReSharper disable Unity.InefficientPropertyAccess

public class PlayerMouseLook : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private LayerMask groundMask;
    
    private Vector2 _mousePosition;
    private Camera _mainCamera;
    
    private void GetMousePosition(Vector2 position) => _mousePosition = position;
    
    private void Awake()
    {
        inputReader.onMousePositionEvent += GetMousePosition;
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }
    
    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        var (success, position) = GetWorldPosition();

        if (!success) return;
        
        var direction = position - transform.position;
        direction.y = 0f;
        transform.forward = direction;
    }


    private (bool success, Vector3 positon) GetWorldPosition()
    {
        // ReSharper disable once PossibleNullReferenceException
        var ray = _mainCamera.ScreenPointToRay(_mousePosition);

        return Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask) ?
            // If raycast hits something, return the position. If not fuck you L + ratio loser
            ( success: true, hitInfo.point) : (success:false, Vector3.zero);
    }
}
