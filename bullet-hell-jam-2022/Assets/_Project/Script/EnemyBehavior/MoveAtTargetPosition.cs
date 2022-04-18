using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAtTargetPosition : MonoBehaviour
{
    public bool _canMove = true;
    [SerializeField] public Vector3 _targetPosition;
    [SerializeField] public float _speed;

    // Update is called once per frame
    void Update()
    {
        if (_canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }
    }
}
