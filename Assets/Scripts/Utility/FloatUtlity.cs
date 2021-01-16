using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatUtlity 
{
    public static int ToMill(this float time)
    {
        return (int)(time * 1000f);
    }
}
