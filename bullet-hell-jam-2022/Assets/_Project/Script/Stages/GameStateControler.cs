using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateControler : MonoBehaviour
{
    [SerializeField] InputScriptableObject _iso;
    [SerializeField] DeathEvent _playerDeathEvent;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
