using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Camera _worldCamera;
    Rigidbody2D _rb;
    public float _followRange = 5.0f;
    public float _extraRange = 5.0f;
    public float _moveSpeed = 5.0f;
    Vector2 _mousePos;
    Vector2 _charPos;
    Vector2 _lightSource;

    void Awake()
    {   
        _rb = GetComponent<Rigidbody2D>();
        SetLightSource(transform.position);
    }

    void Start(){}
    void Update()
    {
        _mousePos = GetMousePosition();
        _charPos = transform.position;
        float theDistance = Vector2.Distance(_charPos, _mousePos);

        if (theDistance <= _followRange)
        {
            float velocity = _moveSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(_charPos, _mousePos, velocity);
        }
        else
        {
            theDistance = Vector2.Distance(_charPos, _lightSource);

            if (theDistance <= _followRange + _extraRange)
            {
                float velocity = _moveSpeed * Time.deltaTime;

                transform.position = Vector2.MoveTowards(_charPos, _lightSource, velocity);
            }
        }
    }

    void FixedUpdate(){}  

    public Vector2 GetMousePosition()
    {
        Vector3 pos = _worldCamera.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;

        return pos;
    }

    public Vector2 GetLightSource()
    {
        return _lightSource;
    }

    public void SetLightSource(Vector2 pos)
    {
        _lightSource = pos;
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(_mousePos, _followRange);
    // }

}
