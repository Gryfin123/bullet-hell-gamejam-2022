using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_script_1 : MonoBehaviour
{

    [SerializeField] private SpawnerSetup _spawnerSetup;
    [SerializeField] private GameObject _prefabLineShooter;
    [SerializeField] private GameObject _prefabSpreader;
    [SerializeField] private GameObject _prefabAssasin;


    private float _currTimer = 0;
    private float _previousTimer = 0;

    // Update is called once per frame
    void Update()
    {
        if (_previousTimer < 5f && _currTimer >= 5f)
        {
            Vector3 startPosition = new Vector3(-8, 8, 0);
            StartCoroutine(WaveToTheRight(startPosition, 10, 0.5f));
        }

        if (_previousTimer < 15f && _currTimer >= 15f)
        {
            StartCoroutine(Wave2());
        }
        
        _previousTimer = _currTimer;
        _currTimer += Time.deltaTime;
    }

    private IEnumerator Wave2()
    {
            void DoIt(string position)
            {
                GameObject created = CreateEnemy(_prefabSpreader, _spawnerSetup.GetPosition(position), 270);
                created.GetComponent<MoveInLine>().enabled = true;
                created.GetComponent<MoveInLine>()._speed = 1.5f;
                created.GetComponent<MoveInLine>()._angle = 180f;
                Destroy(created, 13f);
            }

            DoIt("top_1/8");
            DoIt("top_3/8");
            DoIt("top_5/8");
            DoIt("top_7/8");
            yield return new WaitForSeconds(2f);

    }

    private IEnumerator WaveToTheRight(Vector3 startPosition, float amountEnemies, float enemyDelay)
    {
        float spawnedEnemies = 0;
        while(spawnedEnemies < amountEnemies)
        {
            Vector3 newPosition = startPosition + new Vector3(-1 * spawnedEnemies, 0, 0);
            GameObject created = CreateEnemy(_prefabLineShooter, newPosition, 270);
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
