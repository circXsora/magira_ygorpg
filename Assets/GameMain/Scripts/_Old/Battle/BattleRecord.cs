using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRecord : MGO.SingletonInScene<BattleRecord>
{
    public TMPro.TMP_InputField Record;

    internal void Log(string msg)
    {
        var t = Record.text;
        t = msg + "\n" + t;
        t = t.Substring(0, Math.Min(300, t.Length));
        Record.text = t;
    }
}
