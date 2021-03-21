using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FaildManager : Magia.SingletonInScene<FaildManager>
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public UnityEvent OnReplay;

    public void HanldeReplayButton()
    {
        OnReplay?.Invoke();
    }
}
