using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletFury;
using BulletFury.Data;

[RequireComponent(typeof(Health))]
public class PlayerHealthHandler : MonoBehaviour
{
    private Health _healthComponent;
    
    private Coroutine _regainHpCoroutine = null;

    // Start is called before the first frame update
    void Awaken()
    {
        // Try to fetch health component if not already assigned
        if (_healthComponent == null)
        {
          _healthComponent = GetComponent<Health>();
        }
    }

    private void OnEnable() {
        // Attach events
        _healthComponent.HpDroppedToZero += GameOver;
        _healthComponent.HpChanged += UpdateHealthBar;
        _healthComponent.HpDecreased += GettingHit;
        _healthComponent.HpDecreased += GettingCriticallyHit;
    }

    private void OnDisable() {
        // Attach events
        _healthComponent.HpDroppedToZero -= GameOver;
        _healthComponent.HpChanged -= UpdateHealthBar;
        _healthComponent.HpDecreased -= GettingHit;
        _healthComponent.HpDecreased -= GettingCriticallyHit;
    }

    private void GameOver(float newHp, float damageDealt)
    {
        
    }
    private void UpdateHealthBar(float newHp, float damageDealt)
    {
        
    }
    private void GettingHit(float newHp, float damageDealt)
    {
        
    }
    private void GettingCriticallyHit(float newHp, float damageDealt)
    {
        
    }
}
