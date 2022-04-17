using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAim : MonoBehaviour
{
    public bool _canSpin = true;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject _bulletManagers;

    // Update is called once per frame
    void Update()
    {
        if (_canSpin)
        {
            _bulletManagers.transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        }
    }
}
