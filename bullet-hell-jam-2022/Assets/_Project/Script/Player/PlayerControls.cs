using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] InputScriptableObject _playerInput;

    [Header("Stats")]
    [SerializeField] float _playerSpeedStandard;
    [SerializeField] float _playerSpeedFocus;

    [Header("Warping")]
    [SerializeField] bool _inWarp;
    [SerializeField] LayerMask _realityLayer;
    [SerializeField] LayerMask _warpLayer;

    [Header("Weapons")]
    [SerializeField] List<BulletFury.BulletManager> _weapons;

    

    private void OnEnable() {
        // Attach controls
        _playerInput._warpEvent += OnWarp;
    }
    private void OnDisable() {
        // Attach controls
        _playerInput._warpEvent -= OnWarp;
    }

    private void Update() {
        ProcessMoving();
        if (_playerInput.IsShootinging())
        {
            ProcessShooting();
        }
    }

    private void ProcessMoving()
    {
        Vector3 v3 = _playerInput.GetMovementAxis(); // Convert vecotr 2 to vector 3;
        transform.position += v3 
                              * (_playerInput.IsFocusing() ? _playerSpeedFocus : _playerSpeedStandard)
                              * Time.deltaTime;
    }
    private void ProcessShooting()
    {
        foreach(BulletFury.BulletManager curr in _weapons)
        {
            curr.Spawn(transform.position, Vector3.up);
        }
    }
    private void WarpIn()
    {
        _inWarp = true;
        Debug.Log("Warped");
    }
    private void WarpOut()
    {
        _inWarp = false;
        Debug.Log("Warped out");  
    }



    // EVENT HANDLERS //
    private void OnWarp()
    {
        if (!_inWarp)
        {
            WarpIn();
        }
        else
        {  
            WarpOut();
        }
    }
}
