using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] public GameObject _fadeoutcanvas;
    [SerializeField] public float _timeToSend;

    public void SendToScene(int index)
    {   
        if (_fadeoutcanvas != null) _fadeoutcanvas.SetActive(true);
        StartCoroutine(DelayedLoadScene(index));
    }

    private IEnumerator DelayedLoadScene(int index)
    {
        yield return new WaitForSeconds(_timeToSend);
        SceneManager.LoadScene(index);
    }
}
