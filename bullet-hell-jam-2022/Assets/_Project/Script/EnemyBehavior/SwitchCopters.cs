using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletFury;

public class SwitchCopters : MonoBehaviour
{
    [SerializeField] BulletManager _bm;
    [SerializeField] BulletFury.Data.BulletSettings _bulletSettingsExtreme;
    [SerializeField] BulletFury.Data.SpawnSettings _spawnSettingsExtreme;
    [SerializeField] float _newSpeed = 260;
    [SerializeField] float _speedupDuration = 3f;

    public void SwitchCoptersToExtreme()
    {
        StartCoroutine(SpeedUpCopter());
        _bm.SetBulletSettings(_bulletSettingsExtreme);
        _bm.SetSpawnSettings(_spawnSettingsExtreme);
    }

    private IEnumerator SpeedUpCopter()
    {   
        SpinAim speedComponent = gameObject.GetComponent<SpinAim>();

        while (speedComponent._rotationSpeed < _newSpeed)
        {
            speedComponent._rotationSpeed += Time.deltaTime * _newSpeed / _speedupDuration;
            if (speedComponent._rotationSpeed > _newSpeed)
            {
                speedComponent._rotationSpeed = _newSpeed;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
