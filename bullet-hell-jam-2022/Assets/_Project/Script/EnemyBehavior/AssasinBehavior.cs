using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssasinBehavior : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Health _healthReference;
    [SerializeField] MoveAtTargetPosition _moveAtPosition;
    [SerializeField] Transform _target;
    [SerializeField] SwitchCopters _copterBulletSwitch;
    [SerializeField] HealthbarManagerBoss _hbm;

    [Header("InitialSetup")]
    
    [SerializeField] AimAtTarget[] _aimAtTargets;

    [Header("Weapons")]
    [SerializeField] FireBullets _coopters;
    [SerializeField] FireBullets _machinegunHardcore;
    [SerializeField] FireBullets _machinegunSoftcore;
    [SerializeField] FireBullets _spinAttack;
    [SerializeField] GameObject _rocketPrefab;
    [SerializeField] float _rocketCooldown = 3f;

    [Header("Movement")]
    [SerializeField] float _speed = 2f;

    [Header("Phases")]
    [SerializeField] float _phaseTreshhold1 = 800;
    [SerializeField] float _phaseTreshhold2 = 600;
    [SerializeField] float _phaseTreshhold3 = 400;
    [SerializeField] float _phaseTreshhold4 = 200;
    int _phase = 0;

    [Header("Coroutines")]
    [SerializeField] Coroutine _currPhaseCoroutine;
    [SerializeField] Coroutine _currMovementsCoroutine;

    private void OnEnable() {
        _healthReference.HpChanged += CheckForNewPhase;
        _healthReference.HpChanged += UpdateHealthbar;
    }
    private void OnDisable() {
        _healthReference.HpChanged -= CheckForNewPhase;
        _healthReference.HpChanged -= UpdateHealthbar;
    }

    private void Update() 
    {
        if (_currPhaseCoroutine == null && _phase == 0)
        {
            _currPhaseCoroutine = StartCoroutine(Phase0());
        }
        if (_currPhaseCoroutine == null && _phase == 1)
        {
            _currPhaseCoroutine = StartCoroutine(Phase1());
        }
        else if (_currPhaseCoroutine == null && _phase == 2)
        {
            _currPhaseCoroutine = StartCoroutine(Phase2());
        }
        else if (_currPhaseCoroutine == null && _phase == 3)
        {
            _currPhaseCoroutine = StartCoroutine(Phase3());
        }
        else if (_currPhaseCoroutine == null && _phase == 4)
        {
            _currPhaseCoroutine = StartCoroutine(Phase4());
        }
        else if (_currPhaseCoroutine == null && _phase == 5)
        {
            _currPhaseCoroutine = StartCoroutine(Phase5());
        }
    }

    public void ChangePhase(int newPhase)
    {
        if (_currPhaseCoroutine != null) StopCoroutine(_currPhaseCoroutine);
        _currPhaseCoroutine = null;
        _phase = newPhase;
    }

    private IEnumerator Phase0()
    {
        // Initial settings
        _healthReference.IsInvincible = true;
        _machinegunSoftcore._canFire = false;
        _machinegunHardcore._canFire = false;
        _spinAttack._canFire = false;
        
        Vector3 pos1 = new Vector3(0, 8, 0);
        MoveToThePoint(pos1, _speed);

        yield return new WaitForSeconds(3f);
        // Show healthbar
        Debug.Log("Helathbar became visible");
        _hbm.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);

        _healthReference.IsInvincible = false;
        ChangePhase(1);
    }

    private IEnumerator Phase1()
    {
        // Initial settings
        _machinegunSoftcore._canFire = true;
        _machinegunHardcore._canFire = false;
        _spinAttack._canFire = false;
        
        Vector3 pos1 = new Vector3(-6.5f, 8, 0);
        Vector3 pos2 = new Vector3(6.5f, 8, 0);
        if (_currMovementsCoroutine != null) StopCoroutine(_currMovementsCoroutine);
        _currMovementsCoroutine = StartCoroutine(MovementBetweenTwoPoints(pos1, pos2, _speed));

        while(_phase == 1)
        {
            // _machinegunHardcore._canFire = false;
            yield return new WaitForSeconds(3f);
            // _machinegunHardcore._canFire = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
    private IEnumerator Phase2()
    {
        // Initial settings
        _machinegunSoftcore._canFire = false;
        _machinegunHardcore._canFire = false;
        _spinAttack._canFire = false;
        
        Vector3 pos1 = new Vector3(-2.5f, 8, 0);
        Vector3 pos2 = new Vector3(2.5f, 8, 0);
        if (_currMovementsCoroutine != null) StopCoroutine(_currMovementsCoroutine);
        _currMovementsCoroutine = StartCoroutine(MovementBetweenTwoPoints(pos1, pos2, _speed / 2));
        
        float initialDelay = 3f;
        _healthReference.IsInvincible = true;
        yield return new WaitForSeconds(initialDelay);

        _machinegunHardcore._canFire = true;
        _healthReference.IsInvincible = false;
        while(_phase == 2)
        {
            _spinAttack._canFire = false;
            yield return new WaitForSeconds(4f);
            _spinAttack._canFire = true;
            yield return new WaitForSeconds(1f);
        }
    }
    private IEnumerator Phase3()
    {
        // Initial settings
        _machinegunSoftcore._canFire = false;
        _machinegunHardcore._canFire = false;
        _spinAttack._canFire = true;
        
        Vector3 pos1 = new Vector3(0, 8, 0);
        if (_currMovementsCoroutine != null) StopCoroutine(_currMovementsCoroutine);
        MoveToThePoint(pos1, _speed);

        while(_phase == 3)
        {
            yield return new WaitForSeconds(_rocketCooldown);
        }
    }
    private IEnumerator Phase4()
    {
        // Initial settings
        _machinegunSoftcore._canFire = false;
        _machinegunHardcore._canFire = false;
        _spinAttack._canFire = false;

        float initialDelay = 5f;
        _healthReference.IsInvincible = true;
        yield return new WaitForSeconds(initialDelay);

        _healthReference.IsInvincible = false;
        while(_phase == 4)
        {
            CreateRocket();
            CreateRocket(-25);
            CreateRocket(25);
            yield return new WaitForSeconds(_rocketCooldown * 2);
        }
    }
    private IEnumerator Phase5()
    {
        // Initial settings
        _machinegunSoftcore._canFire = false;
        _machinegunHardcore._canFire = false;
        _spinAttack._canFire = false;

        _healthReference.IsInvincible = true;
        yield return new WaitForSeconds(2);

        _copterBulletSwitch.SwitchCoptersToExtreme();
        yield return new WaitForSeconds(4);
        
        Vector3 pos1 = new Vector3(5.5f, 8, 0);
        Vector3 pos2 = new Vector3(5.5f, -8, 0);
        Vector3 pos3 = new Vector3(-5.5f, -8, 0);
        Vector3 pos4 = new Vector3(-5.5f, 8, 0);
        Vector3 pos5 = new Vector3(0f, 8, 0);

        _healthReference.IsInvincible = false;

        _moveAtPosition._canMove = true;
        _moveAtPosition._speed = _speed * 5;

        float _checkpointPause = 0.5f;
        float _longCheckpointPause = 5f;
        while(_phase == 5)
        {
            _moveAtPosition._targetPosition = pos1;
            while(Vector3.Distance(Vector3.Scale(transform.position, new Vector3(1, 1, 0)), Vector3.Scale(pos1, new Vector3(1, 1, 0))) > 0.2f)
            {
                yield return new WaitForEndOfFrame();
            }      

            _machinegunSoftcore._canFire = true;
            yield return new WaitForSeconds(_checkpointPause);
            _machinegunSoftcore._canFire = false;

            _moveAtPosition._targetPosition = pos2;
            while(Vector3.Distance(Vector3.Scale(transform.position, new Vector3(1, 1, 0)), Vector3.Scale(pos2, new Vector3(1, 1, 0))) > 0.2f)
            {
                yield return new WaitForEndOfFrame();
            }

            _machinegunSoftcore._canFire = true;
            yield return new WaitForSeconds(_checkpointPause);
            _machinegunSoftcore._canFire = false;

            _moveAtPosition._targetPosition = pos3;
            while(Vector3.Distance(Vector3.Scale(transform.position, new Vector3(1, 1, 0)), Vector3.Scale(pos3, new Vector3(1, 1, 0))) > 0.2f)
            {
                yield return new WaitForEndOfFrame();
            }

            _machinegunSoftcore._canFire = true;
            yield return new WaitForSeconds(_checkpointPause);
            _machinegunSoftcore._canFire = false;

            _moveAtPosition._targetPosition = pos4;
            while(Vector3.Distance(Vector3.Scale(transform.position, new Vector3(1, 1, 0)), Vector3.Scale(pos4, new Vector3(1, 1, 0))) > 0.2f)
            {
                yield return new WaitForEndOfFrame();
            }

            _machinegunSoftcore._canFire = true;
            yield return new WaitForSeconds(_checkpointPause);
            _machinegunSoftcore._canFire = false;

            _moveAtPosition._targetPosition = pos5;
            while(Vector3.Distance(Vector3.Scale(transform.position, new Vector3(1, 1, 0)), Vector3.Scale(pos5, new Vector3(1, 1, 0))) > 0.2f)
            {
                yield return new WaitForEndOfFrame();
            }
            
            _machinegunHardcore._canFire = true;
            yield return new WaitForSeconds(_longCheckpointPause);
            _machinegunHardcore._canFire = false;
        }
    }
    
    private IEnumerator MovementBetweenTwoPoints(Vector3 point1, Vector3 point2, float speed)
    {
        _moveAtPosition._canMove = true;
        _moveAtPosition._speed = speed;
        while(true)
        {
            _moveAtPosition._targetPosition = point1;
            while(Vector3.Distance(Vector3.Scale(transform.position, new Vector3(1, 1, 0)), Vector3.Scale(point1, new Vector3(1, 1, 0))) > 0.2f)
            {
                yield return new WaitForEndOfFrame();
            }
            _moveAtPosition._targetPosition = point2;
            while(Vector3.Distance(Vector3.Scale(transform.position, new Vector3(1, 1, 0)), Vector3.Scale(point2, new Vector3(1, 1, 0))) > 0.2f)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private IEnumerator MoveBetween5Points(Vector3 point1, Vector3 point2, Vector3 point3, Vector3 point4, Vector3 point5, float speed)
    {
        _moveAtPosition._canMove = true;
        _moveAtPosition._speed = speed;
        while(true)
        {
            _moveAtPosition._targetPosition = point1;
            while(Vector3.Distance(Vector3.Scale(transform.position, new Vector3(1, 1, 0)), Vector3.Scale(point1, new Vector3(1, 1, 0))) > 0.2f)
            {
                yield return new WaitForEndOfFrame();
            }
            _moveAtPosition._targetPosition = point2;
            while(Vector3.Distance(Vector3.Scale(transform.position, new Vector3(1, 1, 0)), Vector3.Scale(point2, new Vector3(1, 1, 0))) > 0.2f)
            {
                yield return new WaitForEndOfFrame();
            }
            _moveAtPosition._targetPosition = point3;
            while(Vector3.Distance(Vector3.Scale(transform.position, new Vector3(1, 1, 0)), Vector3.Scale(point3, new Vector3(1, 1, 0))) > 0.2f)
            {
                yield return new WaitForEndOfFrame();
            }
            _moveAtPosition._targetPosition = point4;
            while(Vector3.Distance(Vector3.Scale(transform.position, new Vector3(1, 1, 0)), Vector3.Scale(point4, new Vector3(1, 1, 0))) > 0.2f)
            {
                yield return new WaitForEndOfFrame();
            }
            _moveAtPosition._targetPosition = point5;
            while(Vector3.Distance(Vector3.Scale(transform.position, new Vector3(1, 1, 0)), Vector3.Scale(point4, new Vector3(1, 1, 0))) > 0.2f)
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(5f);
        }
    }

    private void MoveToThePoint(Vector3 point1, float speed)
    {
        _moveAtPosition._canMove = true;
        _moveAtPosition._speed = speed;
        _moveAtPosition._targetPosition = point1;
    }

    private void CreateRocket()
    {
        CreateRocket(0);
    }

    private void CreateRocket(float radiusAdjustment)
    {
        Vector3 direction = _target.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + radiusAdjustment;
        Quaternion rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

        GameObject rocket = Instantiate(_rocketPrefab, transform.position, rotation);

        rocket.GetComponent<MoveInLine>()._angle = targetAngle;
        Destroy(rocket, 10f);
    }

    public void InitialSetup(Transform target)
    {
        foreach(AimAtTarget curr in _aimAtTargets)
        {
            curr._target = target;
        }
        _target = target;
    }

    public void UpdateHealthbar(float curr, float damage)
    {
        _hbm.UpdateHp(_healthReference.GetCurrHealth(), _healthReference.GetMaxHealth());
    }

    // Event handlers
    private void CheckForNewPhase(float newHealth, float damage)
    {
        if (newHealth + damage > _phaseTreshhold1 && newHealth <= _phaseTreshhold1)
        {
            ChangePhase(2);
        }
        if (newHealth + damage > _phaseTreshhold2 && newHealth <= _phaseTreshhold2)
        {
            ChangePhase(3);
        }
        if (newHealth + damage > _phaseTreshhold3 && newHealth <= _phaseTreshhold3)
        {
            ChangePhase(4);
        }
        if (newHealth + damage > _phaseTreshhold4 && newHealth <= _phaseTreshhold4)
        {
            ChangePhase(5);
        }
    }
}
