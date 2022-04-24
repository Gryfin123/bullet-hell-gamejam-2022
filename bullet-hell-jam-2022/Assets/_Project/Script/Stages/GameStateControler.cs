using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateControler : MonoBehaviour
{
    [SerializeField] InputScriptableObject _iso;
    [SerializeField] SpawnerSetup _ss;
    [SerializeField] DeathEvent _playerDeathEvent;
    [SerializeField] GameObject _playerPrefab;
    [SerializeField] stage_script_1 _stageScript;

    // Start is called before the first frame update
    void Start()
    {
        _playerDeathEvent.OnDestruction += RestartOnPlayerDeath;
        _iso._quitEvent += CloseGame;
        _iso._restartEvent += RestartStage;
    }

    private void CloseGame()
    {  
        Application.Quit();
    }
    private void RestartStage()
    {
        // Stop game progression
        StopCoroutine(_stageScript._gameflow);

        // Delete enemies
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        // Delete player
        _playerDeathEvent.OnDestruction -= RestartOnPlayerDeath;
        if (_stageScript._playerTransform != null)
        {
            Destroy(_stageScript._playerTransform.gameObject);
        }

        // Create players
        GameObject created = Instantiate(_playerPrefab, new Vector3(0, -8, 0), new Quaternion());
        created.gameObject.GetComponent<PlayerControls>()._levelBoundries = _ss;

        _playerDeathEvent = created.GetComponent<DeathEvent>();
        _playerDeathEvent.OnDestruction += RestartOnPlayerDeath;


        // Start game progression
        _stageScript._playerTransform = created.transform;
        _stageScript.StartGameProgression();
    }

    private void RestartOnPlayerDeath()
    {
        StartCoroutine(RestartCoroutine());
    }

    private IEnumerator RestartCoroutine()
    {
        yield return new WaitForSeconds(3f);
        RestartStage();
    }
}
