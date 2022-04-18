using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyHealthHandler : MonoBehaviour
{
    [SerializeField] private GameObject _bulletManagers;
    [SerializeField] private Health _healthComponent;
    
    private Coroutine _flashCoroutine;
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
         // Make sure bullets wont dissapear together with enemy. 
         // Give them 30 seconds to fly off the screen.
        _bulletManagers.transform.parent = null;
        Destroy(_bulletManagers, 30f);
        Destroy(gameObject);
    }
    private void GettingHit(float newHp, float damageDealt)
    {
        if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
        _flashCoroutine = StartCoroutine(FlashWhite());
    }

    private IEnumerator FlashWhite()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = _defaultColor;
    }
}
