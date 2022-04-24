using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletFury;
using BulletFury.Data;

public class Health : MonoBehaviour
{
    public delegate void Consequense(float newHp, float damageDealt);
    public event Consequense HpDroppedToZero = delegate {};
    public event Consequense HpChanged = delegate {};
    public event Consequense HpIncreased = delegate {};
    public event Consequense HpDecreased = delegate {};

    [SerializeField] float currHealth;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] bool _isInvincible = false;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    public float GetCurrHealth(){
        return currHealth;
    }
    public float GetMaxHealth(){
        return maxHealth;
    }
    public bool IsInvincible{
        get { return _isInvincible; }
        set { _isInvincible = value;}
    }
    
    public void DealDamage(BulletContainer container, BulletCollider collision)
    {
        if (!_isInvincible)
        {
            float amount = container.Damage;
            // Abandon method if hp is already 0
            if (currHealth <= 0) return;

            // Process taking damage
            if (currHealth - amount <= 0)
            {
                HpDroppedToZero.Invoke(0, amount);
                currHealth = 0;
            } 
            else
            {
                currHealth -= amount;
                HpChanged.Invoke(currHealth, amount);
                if (amount < 0)
                {
                HpIncreased.Invoke(currHealth, amount);
                }
                else if (amount > 0)
                {
                HpDecreased.Invoke(currHealth, amount);
                }
            }
        }
    }

    public void Heal(float amount)
    {
        currHealth += amount;
        HpChanged.Invoke(currHealth, amount);
    }
}
