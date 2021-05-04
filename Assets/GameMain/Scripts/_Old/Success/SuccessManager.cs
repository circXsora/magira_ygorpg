using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SuccessManager : MGO.SingletonInScene<SuccessManager>
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public UnityEvent OnNextStage;

    public void NextStage()
    {
        OnNextStage?.Invoke();
    }
}
