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
            float angleInRadians = (_angle + _currRotation) * Mathf.Deg2Rad;
            Vector3 v3 = new Vector2(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians));
            transform.position += v3 * _speed * Time.deltaTime;
            _currRotation += _rotation * Time.deltaTime;
        }
    }
}
