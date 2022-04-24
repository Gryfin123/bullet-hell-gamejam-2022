using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FlashRedBlueLights : MonoBehaviour
{
    [SerializeField] private Volume _volumeReference; 
    private Bloom _bloomReference;
    private float _bloomIntensity;
    [SerializeField] private Color _colorRed;
    [SerializeField] private Color _colorBlue;
    [SerializeField] private float _flashCooldown;

    private void Awake() {
        _volumeReference.profile.TryGet<Bloom>(out _bloomReference);
        _bloomIntensity = _bloomReference.intensity.value;

        StartCoroutine(FlashLightsCoroutine());
    }

    private void Update(){
        //_bloomReference.intensity.value = Mathf.PingPong(Time.time * 2, _bloomIntensity);
    }

    private IEnumerator FlashLightsCoroutine()
    {
        while (true)
        {
            _bloomReference.tint.value = _colorRed;
            yield return new WaitForSeconds(_flashCooldown);
            _bloomReference.tint.value = _colorBlue;
            yield return new WaitForSeconds(_flashCooldown);
        }
    }
}
