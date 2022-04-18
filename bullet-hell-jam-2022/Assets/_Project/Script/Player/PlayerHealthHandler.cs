using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] private Health _healthComponent;
    [SerializeField] private float _timeToRegenerate = 30;
    [SerializeField] private float _timeInvincibility = 2;
    [SerializeField] private GameObject _shieldGameObject;
    

    private Coroutine _regainHpCoroutine = null;
    private Coroutine _invincibilityCoroutine = null;
    private Color _defaultColor;

    // Start is called before the first frame update
    
    void Awake()
    {
        // Try to fetch health component if not already assigned
        _healthComponent = gameObject.GetComponent<Health>();
        _defaultColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void OnEnable() {
        // Attach events
        _healthComponent.HpDroppedToZero += GameOver;
        _healthComponent.HpDecreased += GettingHit;
        _healthComponent.HpDecreased += GettingCriticallyHit;
    }

    private void OnDisable() {
        // Attach events
        _healthComponent.HpDroppedToZero -= GameOver;
        _healthComponent.HpDecreased -= GettingHit;
        _healthComponent.HpDecreased -= GettingCriticallyHit;
    }

    private void GameOver(float newHp, float damageDealt)
    {
        Destroy(gameObject);
        Debug.Log("All hp is lost.");
    }
    private void GettingHit(float newHp, float damageDealt)
    {
        if (!_healthComponent.IsInvincible)
        {
            StartCoroutine(OnHitEffect());
            _invincibilityCoroutine = StartCoroutine(InvincibilityCoroutine());
        }
    }
    private void GettingCriticallyHit(float newHp, float damageDealt)
    {
        if (newHp == 1 && _regainHpCoroutine == null)
        {
            ChangeToCriticallyInjured();
            _regainHpCoroutine = StartCoroutine(RegenerateInjury());
        }
    }

    private void ChangeToCriticallyInjured()
    {
        _shieldGameObject.SetActive(false);
    }
    private void ChangeToNormal()
    {
        _shieldGameObject.SetActive(true);
    }

    private IEnumerator OnHitEffect()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.3f);
        Time.timeScale = 1;
    }
    private IEnumerator InvincibilityCoroutine()
    {
        _healthComponent.IsInvincible = true;
        Coroutine flashing = StartCoroutine(InvincibilityFlashing());
        yield return new WaitForSeconds(_timeInvincibility);
        StopCoroutine(flashing);
        // Make sure flashing will end on correct color.
        gameObject.GetComponent<SpriteRenderer>().color = _defaultColor;
        _healthComponent.IsInvincible = false;
    }
    private IEnumerator InvincibilityFlashing()
    {
        bool flashed = false;
        while(true)
        {
            if (flashed)
            {
                gameObject.GetComponent<SpriteRenderer>().color = _defaultColor;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            }
            flashed = !flashed;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator RegenerateInjury()
    {   
        yield return new WaitForSeconds(_timeToRegenerate);
        if (_healthComponent.GetCurrHealth() != 0)
        {
            _healthComponent.Heal(1);
            ChangeToNormal();
        }
        _regainHpCoroutine = null;
    }
}
