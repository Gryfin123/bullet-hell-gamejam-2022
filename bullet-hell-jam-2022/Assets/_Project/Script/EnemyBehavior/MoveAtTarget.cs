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
            Vector3 target = _target != null ? _target.position : new Vector3(0, 0, 0);
            transform.position = Vector2.MoveTowards(transform.position, target, _speed * Time.deltaTime);
        }
    }
}
