using MGO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalManager : MonoBehaviour
{

    private static GlobalManager instance;
    public static GlobalManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GlobalManager>();
            }
            return instance;
        }
    }

    public WorkManager WorkManager { get; private set; } = new WorkManager();

    public Camera MainCamera;
    public Canvas Canvas;
    public CanvasScaler CanvasScaler;
    public RectTransform CardContainer;
    private void Update()
    {
        WorkManager.Update(Time.deltaTime, Time.unscaledDeltaTime);
    }
}
