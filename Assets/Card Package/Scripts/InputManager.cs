using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public RectTransform RectTransform;
    private Camera cam;
    public event Action<Vector2> OnGetInput;
    public event Action<Vector2> OnGetInputRelease;

    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, Input.mousePosition, cam, out Vector2 localPosition);
            OnGetInput?.Invoke(localPosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, Input.mousePosition, cam, out Vector2 localPosition);
            OnGetInputRelease?.Invoke(localPosition);
        }
    }
}
