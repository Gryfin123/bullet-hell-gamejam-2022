using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public bool _canSpin = true;
    [SerializeField] private float _rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        if (_canSpin)
        {
            transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        }
    }
}
