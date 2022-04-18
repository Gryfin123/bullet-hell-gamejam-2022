using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtTarget : MonoBehaviour
{
    public bool _canSpin = true;
    [SerializeField] public Transform _target;
    [SerializeField] public float _speed;


    // Update is called once per frame
    void Update()
    {
        if (_canSpin)
        {
            Vector2 direction = _target.position - transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _speed * Time.deltaTime);
        }
    }
}
