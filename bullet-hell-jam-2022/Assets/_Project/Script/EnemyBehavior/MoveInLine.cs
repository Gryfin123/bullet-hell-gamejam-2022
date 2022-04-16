using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInLine : MonoBehaviour
{
    public bool _canMove;
    [SerializeField] private float _angle;
    [SerializeField] private float _speed;

    // Update is called once per frame
    void Update()
    {
        if (_canMove)
        {
            float angleInRadians = _angle * Mathf.Deg2Rad;
            Vector3 v3 = new Vector2(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians));
            transform.position += v3 * _speed * Time.deltaTime;
        }
    }
}
