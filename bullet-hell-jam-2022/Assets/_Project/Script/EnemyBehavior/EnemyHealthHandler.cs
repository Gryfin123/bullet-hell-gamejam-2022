using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyHealthHandler : MonoBehaviour
{
    [SerializeField] private GameObject _bulletManagers;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Health _healthComponent;
    
    private Coroutine _flashCoroutine;
    private Color _defaultColor;

    // Start is called before the first frame update
    void Awake()
    {
        // Try to fetch health component if not already assigned
        _healthComponent = gameObject.GetComponent<Health>();
        if (_spriteRenderer == null) _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
    }

    private void OnEnable() {
        // Attach events
        _healthComponent.HpDroppedToZero += AllHpLost;
        _healthComponent.HpDecreased += GettingHit;
    }

    private void OnDisable() {
        // Attach events
        _healthComponent.HpDroppedToZero -= AllHpLost;
        _healthComponent.HpDecreased -= GettingHit;
    }

    private void UpdateColor()
    {
        // ...
    }

    private void AllHpLost(float newHp, float damageDealt)
    {
        Destroy(gameObject);
    }
    private void GettingHit(float newHp, float damageDealt)
    {
        if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
        _flashCoroutine = StartCoroutine(FlashWhite());
    }

    private IEnumerator FlashWhite()
    {
        _spriteRenderer.color = new Color(255,255,255);
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = _defaultColor;
    }
}
