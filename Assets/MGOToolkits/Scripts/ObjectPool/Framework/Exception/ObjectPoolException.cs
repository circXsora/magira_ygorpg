using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPoolException : System.Exception
{
    public string PoolName { get; set; }
    public ObjectPoolException(string poolName) { PoolName = poolName; }
}
