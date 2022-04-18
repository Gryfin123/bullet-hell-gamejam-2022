using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_script_1 : MonoBehaviour
{

    [SerializeField] private GameObject _prefabLineShooter;
    [SerializeField] private GameObject _prefabSpreader;
    [SerializeField] private GameObject _prefabAssasin;


    private float _currTimer = 0;
    private float _previousTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float waveT1 = 5f;
        if (_previousTimer < waveT1 && _currTimer >= waveT1)
        {
            Vector3 startPosition = new Vector3(-8, 8, 0);
            StartCoroutine(Wave1(startPosition, 10, 0.5f));
        }
        
        _previousTimer = _currTimer;
        _currTimer += Time.deltaTime;
    }

    private IEnumerator Wave1(Vector3 startPosition, float amountEnemies, float enemyDelay)
    {
        float spawnedEnemies = 0;
        while(spawnedEnemies < amountEnemies)
        {
            Vector3 newPosition = startPosition + new Vector3(-1 * spawnedEnemies, 0, 0);
            GameObject created = CreateEnemy(_prefabLineShooter, newPosition, 2070);
            created.GetComponent<MoveInLine>().enabled = true;
            created.GetComponent<MoveInLine>()._speed = 1.5f;
            created.GetComponent<MoveInLine>()._angle = 90f;
            Destroy(created, 30f);
            
            spawnedEnemies++;
        }
        yield return new WaitForSeconds(enemyDelay);
    }

    private GameObject CreateEnemy(GameObject prefab, Vector3 position, float angleDegree)
    {
        GameObject newlyMade = Instantiate(prefab, position, new Quaternion());
        newlyMade.transform.Rotate(Vector3.forward * angleDegree);
        return newlyMade;
    }
}
