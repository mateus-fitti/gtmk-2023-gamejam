using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public LevelController levelCtrl;

    Collider2D _collider;
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Abigail")
        {
            this.Endgame();
        }
    }

    void Endgame()
    {
        levelCtrl.Victory();
    }
}
