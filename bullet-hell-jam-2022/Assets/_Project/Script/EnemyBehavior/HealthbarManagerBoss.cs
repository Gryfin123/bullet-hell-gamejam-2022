using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarManagerBoss : MonoBehaviour
{
    [SerializeField] Image[] _healthbars;
    
    public void UpdateHp(float currHp, float maxHp)
    {
        float remainingDamage = maxHp - currHp;

        //divid max between all healthbars
        Debug.Log("Updating healthbars");
        float maxPerHealthbar = maxHp / _healthbars.Length;

        foreach(Image healthbar in _healthbars)
        {
            if (remainingDamage >= maxPerHealthbar)
            {
                healthbar.fillAmount = 0;
                remainingDamage -= maxPerHealthbar;
            }
            else
            {
                healthbar.fillAmount = (maxPerHealthbar - remainingDamage) / maxPerHealthbar;
                break;
            }
        }


    }
}
