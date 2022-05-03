using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MGO;
namespace BBYGO
{

    public interface IInputManager
    {

    }

    public class InputManager : GameModule, IInputManager
    {
        
        public event Action<Vector2> OnGetInput;
        public event Action<Vector2> OnGetInputRelease;

        public int Priority => -1000;

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (Input.GetMouseButton(0))
            {
                //RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, Input.mousePosition, Cam, out Vector2 localPosition);
                //OnGetInput?.Invoke(localPosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                //RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, Input.mousePosition, Cam, out Vector2 localPosition);
                //OnGetInputRelease?.Invoke(localPosition);
            }
        }
    }

}