using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_script_1 : MonoBehaviour
{

    [SerializeField] private SpawnerSetup _spawnerSetup;
    [SerializeField] private Transform _playerTransform;
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
            StartCoroutine(FlybyMedium());
        }

        if (_previousTimer < 15f && _currTimer >= 15f)
        {
            
        }
        
        _previousTimer = _currTimer;
        _currTimer += Time.deltaTime;
    }

    // Waves
    private IEnumerator FlybySimple()
    {
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator FlybyMedium()
    {
        GameObject prefLine = Resources.Load<GameObject>("PrefabEnemies/en_line_shooter");
        GameObject prefDouble = Resources.Load<GameObject>("PrefabEnemies/en_double");
        float spawnDelay = 3f;
        
        DoIt(prefLine, "left_2/2", new Vector3(6, -5, 0));
        DoIt(prefLine, "right_2/2", new Vector3(-6, -5, 0));
        yield return new WaitForSeconds(spawnDelay);

        DoIt(prefDouble, "left_2/2", new Vector3(6, -3, 0));
        DoIt(prefDouble, "right_2/2", new Vector3(-6, -3, 0));
        yield return new WaitForSeconds(spawnDelay);

        DoIt(prefLine, "left_2/2", new Vector3(6, -1, 0));
        DoIt(prefLine, "right_2/2", new Vector3(-6, -1, 0));
        yield return new WaitForSeconds(spawnDelay);

        DoIt(prefDouble, "left_2/2", new Vector3(6, 1, 0));
        DoIt(prefDouble, "right_2/2", new Vector3(-6, 1, 0));
        yield return new WaitForSeconds(spawnDelay);

        DoIt(prefLine, "left_2/2", new Vector3(6, 3, 0));
        DoIt(prefLine, "right_2/2", new Vector3(-6, 3, 0));
        yield return new WaitForSeconds(spawnDelay);

        DoIt(prefDouble, "left_2/2", new Vector3(6, 5, 0));
        DoIt(prefDouble, "right_2/2", new Vector3(-6, 5, 0));
        yield return new WaitForSeconds(spawnDelay);

        DoIt(prefLine, "left_2/2", new Vector3(6, 7, 0));
        DoIt(prefLine, "right_2/2", new Vector3(-6, 7, 0));
        yield return new WaitForSeconds(spawnDelay);

        DoIt(prefDouble, "left_2/2", new Vector3(6, 9, 0));
        DoIt(prefDouble, "right_2/2", new Vector3(-6, 9, 0));
        yield return new WaitForSeconds(spawnDelay);


        void DoIt(GameObject prefab, string startPosition, Vector3 targetPosition)
        {
            GameObject created = CreateEnemy(prefab, _spawnerSetup.GetPosition(startPosition), 270);
            created.GetComponent<MoveAtTargetPosition>().enabled = true;
            created.GetComponent<MoveAtTargetPosition>()._targetPosition = targetPosition;
            created.GetComponent<MoveAtTargetPosition>()._speed = 1f;

            created.GetComponent<AimAtTarget>().enabled = true;
            created.GetComponent<AimAtTarget>()._target = _playerTransform;
        }
    }

    private IEnumerator TestWave2()
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

    private IEnumerator TestWave1(Vector3 startPosition, float amountEnemies, float enemyDelay)
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
