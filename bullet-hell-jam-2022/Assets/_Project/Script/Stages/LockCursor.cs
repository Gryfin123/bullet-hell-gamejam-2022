using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCursor : MonoBehaviour
{
    [SerializeField] bool _cursorLocked = false;
    // Start is called before the first frame update
    void Start()
    {
        if (_cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
