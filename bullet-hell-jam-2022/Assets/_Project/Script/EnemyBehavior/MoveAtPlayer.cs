using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAtPlayer : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _speed;
    [SerializeField] bool _canMove;

    // Update is called once per frame
    void Update()
    {
        if (_canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
    }
}
