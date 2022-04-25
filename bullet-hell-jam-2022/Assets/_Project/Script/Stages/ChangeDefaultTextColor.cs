using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeDefaultTextColor : MonoBehaviour
{
    [SerializeField] Color _textColor;
    [SerializeField] Color _outlineColor;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TMP_Text>().color = _textColor;
        gameObject.GetComponent<TMP_Text>().outlineColor = _outlineColor;
    }
}
