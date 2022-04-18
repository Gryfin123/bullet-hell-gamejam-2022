using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTeleportBack : MonoBehaviour
{
    [SerializeField] private List<GameObject> _backgroundPanels;
    [SerializeField] private float _treshold = -20f;
    [SerializeField] private float _scrollSpeed = -0.5f;
    [SerializeField] private float _pushback = 30f;

    private void Awake() {
        if (_backgroundPanels == null) _backgroundPanels = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject panel in _backgroundPanels)
        {
            panel.transform.position += Vector3.up * _scrollSpeed * Time.deltaTime;
            if (panel.transform.position.y <= _treshold)
            {
                panel.transform.position += Vector3.up * _pushback;
            }
        }

    }
}
