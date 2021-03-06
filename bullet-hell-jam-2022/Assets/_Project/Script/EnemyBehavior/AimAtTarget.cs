using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtTarget : MonoBehaviour
{
    public bool _canSpin = true;
    [SerializeField] public Transform _target;
    [SerializeField] public float _angleOffset = 0;
    [SerializeField] public float _speed = 1000;


    // Update is called once per frame
    void Update()
    {
        if (_canSpin)
        {
            Vector3 target = _target != null ? _target.position : new Vector3(0, 0, 0);

            Vector2 direction = target - transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - _angleOffset;
            Quaternion rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _speed * Time.deltaTime);

        }
    }
}
