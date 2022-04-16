using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _speed;


    // Update is called once per frame
    void Update()
    {
        Vector2 direction = _target.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _speed * Time.deltaTime);
    }
}
