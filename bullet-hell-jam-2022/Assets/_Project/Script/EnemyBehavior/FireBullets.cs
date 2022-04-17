using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{
    public bool _canFire = true;
    [SerializeField] private List<BulletFury.BulletManager> _bulletManager; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_canFire)
        {
            foreach(BulletFury.BulletManager curr in _bulletManager)
            {
                curr.Spawn(transform.position, curr.transform.up);
            }
        }
    }

}
