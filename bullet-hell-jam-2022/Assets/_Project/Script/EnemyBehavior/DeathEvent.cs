using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEvent : MonoBehaviour
{
    public delegate void Death();
    public event Death OnDestruction = delegate {};
    
    private void OnDestroy() {
        OnDestruction.Invoke();
    }
}
