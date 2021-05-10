using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleTrigger : MonoBehaviour
{
    public event EventHandler TriggerWithPlayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TriggerWithPlayer?.Invoke(this, null);
        }
    }
}
