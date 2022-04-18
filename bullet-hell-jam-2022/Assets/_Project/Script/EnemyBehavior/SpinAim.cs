using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAim : MonoBehaviour
{
    public bool _canSpin = true;
    [SerializeField] public float _rotationSpeed;
    [SerializeField] public GameObject _bulletManagers;

    // Update is called once per frame
    void Update()
    {
        if (_canSpin)
        {
            _bulletManagers.transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        }
    }
}
