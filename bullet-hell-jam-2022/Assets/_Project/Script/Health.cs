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
    
    public void DealDamage(BulletContainer container, BulletCollider collision){

        float amount = container.Damage;

        // Abandon method if hp is already 0
        if (currHealth <= 0) return;

        // Process taking damage
        if (currHealth - amount <= 0)
        {
            currHealth = 0;
            HpDroppedToZero(0, amount);
        } 
        else
        {
            currHealth -= amount;
            HpChanged(currHealth, amount);
            if (amount < 0)
            {
              HpIncreased(currHealth, amount);
            }
            else if (amount > 0)
            {
              HpDecreased(currHealth, amount);
            }
        }
    }
}
