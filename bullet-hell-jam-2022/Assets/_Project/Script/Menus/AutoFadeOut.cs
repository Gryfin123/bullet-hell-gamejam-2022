using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoFadeOut : MonoBehaviour
{
    [SerializeField] GameObject _targetCanvas;
    [SerializeField] Image[] _targetImages;
    [SerializeField] float _fadeSpeed = 0.5f;
    [SerializeField] float _delay = 3f;
    float _currTimer = 0f;
    
    // Update is called once per frame
    void Update()
    {
        int disabledImg = 0;
        int totalImg = 0;
        if (_currTimer > _delay)
        {
            foreach(Image img in _targetImages)
            {
                totalImg++;
                img.color = new Color(img.color.r, img.color.b, img.color.g, img.color.a - _fadeSpeed * Time.deltaTime);
                if (img.color.a <= 0)
                {
                    img.enabled = false;
                    disabledImg++;
                }
            }

        }
        
        if (totalImg == disabledImg && totalImg != 0)
        {
            Destroy(_targetCanvas);
        }
        _currTimer += Time.deltaTime;
    }
}
