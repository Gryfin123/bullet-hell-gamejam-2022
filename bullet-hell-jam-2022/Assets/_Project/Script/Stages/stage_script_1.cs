using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class stage_script_1 : MonoBehaviour
{
    [Header("Arsenal")]
    [SerializeField] private SpawnerSetup _spawnerSetup;
    [SerializeField] private GameObject _prefabLineShooter;
    [SerializeField] private GameObject _prefabDoubleShooter;
    [SerializeField] private GameObject _prefabSpreader;
    [SerializeField] private GameObject _prefabAssasin;
    [SerializeField] private GameObject _prefabBoss;
    
    [Header("Tutorial")]
    [SerializeField] private GameObject _tutorialCanvas;
    [SerializeField] private InputScriptableObject _input;
    [SerializeField] private Color _checkColor;
    [SerializeField] private TMP_Text _textMove;
    private bool _didMove = false;
    [SerializeField] private TMP_Text _textShoot;
    private bool _didShoot = false;
    [SerializeField] private TMP_Text _textSlowdown;
    private bool _didSlowdown = false;
    [SerializeField] private TMP_Text _textDestroyTargets;
    private bool _didDestroyTarget = false;
    [SerializeField] private TMP_Text _textRememberToNotGetKilled;
    private bool _didRemeber = false;


    [Header("Player")]
    [SerializeField] public GameObject _fadoutCanvas;
    [SerializeField] public Transform _playerTransform;
    [SerializeField] public int _startingCheckpoint = 0;
    public Coroutine _gameflow;


    private float _timerCountdown = 5f;
    private int _currEnemyCount = 0;
    private int _currCheckpoint = 0;

    private void Start() 
    {    
        _currCheckpoint = _startingCheckpoint;
        StartGameProgression();
    }

    public void StartGameProgression()
    {
        _gameflow = StartCoroutine(GameFlow());
    }

    // Waves 
    private IEnumerator GameFlow()
    {
        yield return new WaitForSeconds(0.5f);
        if (_currCheckpoint == 0)
        {
            _tutorialCanvas.SetActive(true);
            // create dummy guys
            CreateTargetPracticeEnemy("top_1/4", new Vector3(-3, 6, 0), 1);
            CreateTargetPracticeEnemy("top_3/4", new Vector3(3, 6, 0), 1);
            CreateTargetPracticeEnemy("left_1/4", new Vector3(-3, -6, 0), 1);
            CreateTargetPracticeEnemy("right_1/4", new Vector3(3, -6, 0), 1);


            bool _movedVertically = false;
            bool _movedHorizontally = false;
            
            while (!(_didMove && _didShoot && _didSlowdown && _didDestroyTarget && _didRemeber))
            {
                Vector3 input = (Vector3)_input.GetMovementAxis();
                if (input.x != 0) _movedHorizontally = true; 
                if (input.y != 0) _movedVertically = true;
                if (_movedHorizontally && _movedVertically)
                {
                    TutCheckMovement();
                }

                if (_input.IsShootinging())
                {
                    TutCheckShoot();
                }

                if (_input.IsFocusing())
                {
                    TutCheckSlowdown();
                }

                if (_currEnemyCount == 0)
                {
                    TutCheckDestroyTargets();
                }

                if (_didMove && _didShoot && _didSlowdown && _didDestroyTarget)
                {
                    yield return new WaitForSeconds(4f);
                    TutCheckRemember();
                }

                yield return new WaitForEndOfFrame();
            }

            // set checkpoin
            SetCheckpoint(1);

            yield return new WaitForSeconds(2f);
            _tutorialCanvas.SetActive(false);
        }
        
        yield return new WaitForSeconds(4f);

        // Stage 1
        if (_currCheckpoint == 1)
        {
            for(int i = 0; i < 6; i++)
            {
                CreateNonShootingEnemy("right_7/8", 180, 5, 2);
                yield return new WaitForSeconds(0.5f);
            }
            for(int i = 0; i < 6; i++)
            {
                CreateNonShootingEnemy("left_7/8", 0, -5, 2);
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(2);

            for(int i = 0; i < 5; i++)
            {
                CreateShooters1(_prefabLineShooter, "right_7/8", 180, 0, 2);
                yield return new WaitForSeconds(1f);

                CreateShooters1(_prefabLineShooter, "left_6/8", 0, 0, 2);
                yield return new WaitForSeconds(1f);

                CreateShooters1(_prefabDoubleShooter, "right_7/8", 180, 0, 2);
                yield return new WaitForSeconds(1f);

                CreateShooters1(_prefabDoubleShooter, "left_6/8", 0, 0, 2);
                yield return new WaitForSeconds(1f);
            }

            CreateShooters2(_prefabSpreader, "top_1/4", new Vector3(-5, 8, 0), 2);
            yield return new WaitForSeconds(1.3f);
            CreateShooters2(_prefabSpreader, "top_2/4", new Vector3(0, 8, 0), 2);
            yield return new WaitForSeconds(1.3f);
            CreateShooters2(_prefabSpreader, "top_3/4", new Vector3(5, 8, 0), 2);
            yield return new WaitForSeconds(1.3f);

            while (_currEnemyCount > 0)
            {
                yield return new WaitForEndOfFrame();
            }
            SetCheckpoint(2);
        }

        // Stage 2
        if (_currCheckpoint == 2)
        {
            for(int i = 0; i < 8; i++)
            {
                CreateShooters3(_prefabLineShooter, "right_" + i + "/8", 180, -45, 2);
                CreateShooters3(_prefabLineShooter, "left_" + i + "/8", 0, 45, 2);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(10f);

            for(int i = 1; i <= 5; i++)
            {
                CreateCopters(_prefabLineShooter, "top_" + i + "/6", 270, 0, 2);
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(10f);

            for(int j = 0; j < 3; j++)
            {
                for(int i = 1; i < 4; i++)
                {
                    CreateCopters(_prefabAssasin, "top_" + i + "/4", 270, 0, 1.5f);
                }
                yield return new WaitForSeconds(5f);
            }

            while (_currEnemyCount > 0)
            {
                yield return new WaitForEndOfFrame();
            }
            
            SetCheckpoint(3);
        }

        // Stage 3
        if (_currCheckpoint == 3)
        {
            GameObject boss = CreateEnemyBasic(_prefabBoss, _spawnerSetup.GetPosition("top_1/2"), 0, 9999);
            boss.GetComponent<AssasinBehavior>().InitialSetup(_playerTransform);

            while (_currEnemyCount > 0)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        // All done
        _fadoutCanvas.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);
        
        // Functions
        void CreateTargetPracticeEnemy(string startPosition, Vector3 targetPosition, float speed)
        {
            GameObject created = CreateEnemyBasic(_prefabLineShooter, _spawnerSetup.GetPosition(startPosition), 270, 9999);
            created.GetComponent<MoveAtTargetPosition>().enabled = true;
            created.GetComponent<MoveAtTargetPosition>()._targetPosition = targetPosition;
            created.GetComponent<MoveAtTargetPosition>()._speed = speed;

            created.GetComponent<FireBullets>().enabled = false;
            created.GetComponent<AimAtTarget>().enabled = true;
            created.GetComponent<AimAtTarget>()._target = _playerTransform;
        }
        void CreateNonShootingEnemy(string startPosition, float direction, float rotation, float speed)
        {
            GameObject created = CreateEnemyBasic(_prefabLineShooter, _spawnerSetup.GetPosition(startPosition), 270, 20);
            created.GetComponent<MoveInLine>().enabled = true;
            created.GetComponent<MoveInLine>()._angle = direction;
            created.GetComponent<MoveInLine>()._rotation = rotation;
            created.GetComponent<MoveInLine>()._speed = speed;

            created.GetComponent<FireBullets>().enabled = false;
            created.GetComponent<AimAtTarget>().enabled = true;
            created.GetComponent<AimAtTarget>()._target = _playerTransform;
        }
        void CreateShooters1(GameObject prefab, string startPosition, float direction, float rotation, float speed)
        {
            GameObject created = CreateEnemyBasic(prefab, _spawnerSetup.GetPosition(startPosition), 270, 20);
            created.GetComponent<MoveInLine>().enabled = true;
            created.GetComponent<MoveInLine>()._angle = direction;
            created.GetComponent<MoveInLine>()._rotation = rotation;
            created.GetComponent<MoveInLine>()._speed = speed;

            created.GetComponent<FireBullets>().enabled = true;
            created.GetComponent<FireBullets>()._fireLimit = 3;
            created.GetComponent<FireBullets>()._initialFireDelay = 1.5f;
            created.GetComponent<AimAtTarget>().enabled = true;
            created.GetComponent<AimAtTarget>()._target = _playerTransform;
        }
        void CreateShooters2(GameObject prefab, string startPosition, Vector3 targetPosition, float speed)
        {
            GameObject created = CreateEnemyBasic(prefab, _spawnerSetup.GetPosition(startPosition), 270, 999);
            created.GetComponent<MoveAtTargetPosition>().enabled = true;
            created.GetComponent<MoveAtTargetPosition>()._targetPosition = targetPosition;
            created.GetComponent<MoveAtTargetPosition>()._speed = speed;

            created.GetComponent<FireBullets>().enabled = true;
            created.GetComponent<FireBullets>()._initialFireDelay = 6;
            created.GetComponent<AimAtTarget>().enabled = true;
            created.GetComponent<AimAtTarget>()._target = _playerTransform;
        }
        void CreateShooters3(GameObject prefab, string startPosition, float direction, float rotation, float speed)
        {
            GameObject created = CreateEnemyBasic(prefab, _spawnerSetup.GetPosition(startPosition), direction, 35);
            created.GetComponent<MoveInLine>().enabled = true;
            created.GetComponent<MoveInLine>()._angle = direction;
            created.GetComponent<MoveInLine>()._rotation = rotation;
            created.GetComponent<MoveInLine>()._speed = speed;

            created.GetComponent<FireBullets>().enabled = true;
            created.GetComponent<FireBullets>()._initialFireDelay = 1f;
        }
        void CreateCopters(GameObject prefab, string startPosition, float direction, float rotation, float speed)
        {
            GameObject created = CreateEnemyBasic(prefab, _spawnerSetup.GetPosition(startPosition), direction, 20);
            created.GetComponent<MoveInLine>().enabled = true;
            created.GetComponent<MoveInLine>()._angle = direction;
            created.GetComponent<MoveInLine>()._rotation = rotation;
            created.GetComponent<MoveInLine>()._speed = speed;

            created.GetComponent<FireBullets>().enabled = true;
        }
    }



    // Waves tests
    private IEnumerator FlybyMedium()
    {
        GameObject prefLine = _prefabLineShooter;
        GameObject prefDouble = _prefabDoubleShooter;
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
            GameObject created = CreateEnemyBasic(prefab, _spawnerSetup.GetPosition(startPosition), 270, 9999);
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
                GameObject created = CreateEnemyBasic(_prefabSpreader, _spawnerSetup.GetPosition(position), 270, 13);
                created.GetComponent<MoveInLine>().enabled = true;
                created.GetComponent<MoveInLine>()._speed = 1.5f;
                created.GetComponent<MoveInLine>()._angle = 180f;
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
            GameObject created = CreateEnemyBasic(_prefabLineShooter, newPosition, 270, 30);
            created.GetComponent<MoveInLine>().enabled = true;
            created.GetComponent<MoveInLine>()._speed = 1.5f;
            created.GetComponent<MoveInLine>()._angle = 90f;
            
            spawnedEnemies++;
        }
        yield return new WaitForSeconds(enemyDelay);
    }

    // Tutorial Function
    private void TutCheckMovement()
    {
        _didMove = true;
        _textMove.outlineColor = _checkColor;
    }
    private void TutCheckShoot()
    {
        _didShoot = true;
        _textShoot.outlineColor = _checkColor;
    }
    private void TutCheckSlowdown()
    {
        _didSlowdown = true;
        _textSlowdown.outlineColor = _checkColor;
    }
    private void TutCheckDestroyTargets()
    {
        _didDestroyTarget = true;
        _textDestroyTargets .outlineColor = _checkColor;
    }
    private void TutCheckRemember()
    {
        _didRemeber = true;
        _textRememberToNotGetKilled.outlineColor = _checkColor;
    }


    // Utilities
    private GameObject CreateEnemyBasic(GameObject prefab, Vector3 position, float angleDegree, float destructionTime)
    {
        GameObject newlyMade = Instantiate(prefab, position, new Quaternion());
        newlyMade.transform.Rotate(Vector3.forward * angleDegree);
        newlyMade.transform.parent = gameObject.transform;

        AddToEnemyCount();
        newlyMade.GetComponent<DeathEvent>().OnDestruction += DecreaseEnemyCount;
        Destroy(newlyMade, destructionTime);

        return newlyMade;
    }

    private void SetCheckpoint(int newCheckpoint)
    {
        _currCheckpoint = newCheckpoint;
    }
    private void AddToEnemyCount()
    {
        _currEnemyCount++;
        //Debug.Log("Added enemy (" + _currEnemyCount + ")");
    }
    private void DecreaseEnemyCount()
    {
        _currEnemyCount--;
        //Debug.Log("Removed enemy (" + _currEnemyCount + ")");
    }
}

