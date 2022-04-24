using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{
    public bool _canFire = true;
    public int _fireLimit = 0; // Zero means there is no limit
    public float _initialFireDelay = 0f;
    private float _creationTime;
    [SerializeField] private List<BulletFury.BulletManager> _bulletManager; 
    private int _timesFired = 0;

    // Start is called before the first frame update
    void Start()
    {
        _creationTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_canFire 
            && (_fireLimit == 0 || _fireLimit > _timesFired) 
            && _creationTime + _initialFireDelay < Time.time)
        {
            foreach(BulletFury.BulletManager curr in _bulletManager)
            {
                curr.Spawn(curr.gameObject.transform.position, curr.transform.up);
            }
        }
    }

    public void CountFires()
    {
        _timesFired++;
    }
}
