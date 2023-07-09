using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FearBar : MonoBehaviour
{

    public TextMeshProUGUI _textBar; // Change to a filler bar
    CharacterMovement cMove; // To get character atributes

    float _fearRange;
    int _fearLimit = 100;
    int _fear = 0;
    float _counter = 0f;
    public float _fearDelay = 1.0f;
    public int _fearDefaultVal = 1;

    void Start(){}

    void Awake()
    {   
        cMove = GetComponent<CharacterMovement>();
        _fearRange = cMove._followRange;
        UpdateFear(0);
    }

    void Update()
    {
        Vector2 mousePos = cMove.GetMousePosition();
        Vector2 charPos = transform.position;
        Vector2 lightPos = cMove.GetLightSource();

        float dist1 = Vector2.Distance(charPos, mousePos);
        float dist2 = Vector2.Distance(charPos, lightPos);

        int fearValue = 0;

        if (dist2 <= _fearRange)
        {
            fearValue -= _fearDefaultVal;
        }
        else if (dist1 <= _fearRange)
        {
            fearValue = 0;
        }
        else
        {
            fearValue += _fearDefaultVal;
        }

        _counter += Time.deltaTime;

        if (_counter >= _fearDelay)
        {
            _counter = 0f;

            UpdateFear(fearValue);
        }
    }

    void UpdateFear(int value)
    {
        _fear += value;

        if (_fear >= _fearLimit)
        {
            _fear = _fearLimit;
            Defeat();
        }

        if (_fear <= 0)
        {
            _fear = 0;
        }

        _textBar.text = _fear + "/" + _fearLimit;
        Debug.Log("O nivel de medo é " + _textBar.text);
    }

    void Defeat()
    {
        // Show defeat screen
    }

}
