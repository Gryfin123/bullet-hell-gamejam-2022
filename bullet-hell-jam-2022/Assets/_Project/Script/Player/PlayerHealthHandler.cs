using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] private Health _healthComponent;
    [SerializeField] private float _timeInvincibility = 2;
    [SerializeField] private GameObject _shieldGameObject;
    
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
    }

    private void OnDisable() {
        // Attach events
        _healthComponent.HpDroppedToZero -= GameOver;
        _healthComponent.HpDecreased -= GettingHit;
    }

    private void GameOver(float newHp, float damageDealt)
    {
        StartCoroutine(OnHitEffect());
    }
    private void GettingHit(float newHp, float damageDealt)
    {
        StartCoroutine(OnHitEffect());
        _invincibilityCoroutine = StartCoroutine(InvincibilityCoroutine());
    }
    private IEnumerator OnHitEffect()
    {
        Time.timeScale = 0;
        float stopTime = 0.3f;
        Coroutine visual = StartCoroutine(OnHitVisualEffect(stopTime));
        yield return new WaitForSecondsRealtime(stopTime);
        Time.timeScale = 1;

        if (_healthComponent.GetCurrHealth() == 0)
        {                
            Debug.Log("All hp is lost.");
            Destroy(gameObject);
        }
    }
    private IEnumerator OnHitVisualEffect(float duration)
    {
        float startSize = 5;
        _shieldGameObject.transform.localScale = new Vector3(startSize, startSize, 0);
        _shieldGameObject.SetActive(true);
        float reductionValue = startSize * 1f / 60f * 1f / duration / 10f;
        while(_shieldGameObject.transform.localScale.x > 0)
        {
            _shieldGameObject.transform.localScale -= new Vector3(reductionValue, reductionValue, 0);
            yield return new WaitForEndOfFrame();
        }

        _shieldGameObject.transform.localScale = new Vector3(0,0,0);
        _shieldGameObject.SetActive(false);
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
}
