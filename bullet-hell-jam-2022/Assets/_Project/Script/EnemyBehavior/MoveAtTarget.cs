using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAtTarget : MonoBehaviour
{
    public bool _canMove = true;
    [SerializeField] public Transform _target;
    [SerializeField] public float _speed;

    // Update is called once per frame
    void Update()
    {
        if (_canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
    }
}
