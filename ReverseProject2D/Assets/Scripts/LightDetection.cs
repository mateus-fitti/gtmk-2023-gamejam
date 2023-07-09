using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    CircleCollider2D _collider;
    CharacterMovement cMove;

    void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        cMove = GetComponentInParent<CharacterMovement>();

        _collider.radius = cMove._followRange;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "LightSource")
        {
            Debug.Log("LUZ AQUI!");
            cMove.SetLightSource(collision.transform.position);
        }
    }

}
