using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInLine : MonoBehaviour
{
    public bool _canMove = true;
    [SerializeField] public float _rotation;
    private float _currRotation = 0;
    [SerializeField] public float _angle;
    [SerializeField] public float _speed;

    // Update is called once per frame
    void Update()
    {
        if (_canMove)
        {
            float angleInRadians = ((_angle + _currRotation)) * Mathf.Deg2Rad;
            transform.position += new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0) * _speed * Time.deltaTime;
            _currRotation += _rotation * Time.deltaTime;
        }
    }
}
