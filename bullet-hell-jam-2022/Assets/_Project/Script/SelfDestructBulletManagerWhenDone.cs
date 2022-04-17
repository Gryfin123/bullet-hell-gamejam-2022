using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletFury.BulletManager))]
public class SelfDestructBulletManagerWhenDone : MonoBehaviour
{
    private BulletFury.BulletManager _bm;

    private void Start() {
        _bm = GetComponent<BulletFury.BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent == null && !_bm.CheckBulletsRemaining())
        {
            Destroy(gameObject);
        }
    }
}
