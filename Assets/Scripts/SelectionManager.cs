using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";

    private ISelectionResponse _selectionResponse;

    private Transform _selection;

    private Camera _camera;

    [SerializeField] private float distance = 10;
    private void Awake()
    {
        _selectionResponse = GetComponent<ISelectionResponse>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_selection != null) _selectionResponse.OnDeselect(_selection);
        
        var screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        // Create a ray from the camera through the center of the screen
        var ray = _camera.ScreenPointToRay(screenCenter);
        _selection = null;
        if (Physics.Raycast(ray, out var hit,distance))
        {
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                _selection = selection;
            }
        }

        if (_selection != null) _selectionResponse.OnSelect(_selection);
    }

  
}