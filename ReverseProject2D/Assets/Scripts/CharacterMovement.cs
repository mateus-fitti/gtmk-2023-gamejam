using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class CharacterMovement : MonoBehaviour
{
    public Camera _worldCamera;
    Rigidbody2D _rb;
    public float _followRange = 5.0f;
    public float _moveSpeed = 5.0f;

    public GameObject secretScreen;
    public List<TipoChave> playerKeys = new List<TipoChave>();
    Vector2 _mousePos;
    Vector2 _charPos;
    Vector2 _lightSource;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        SetLightSource(transform.position);
    }

    void Start() { }
    void Update()
    {
        _mousePos = GetMousePosition();
        _charPos = transform.position;

        if (IsCollidingWithGhost())
        {
            MoveTowards(_mousePos);
        }
        else if (IsCollidingWithLightSource())
        {
            MoveTowards(_lightSource);
        }
    }

    bool IsCollidingWithGhost()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(_charPos);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Ghost"))
            {
                return true;
            }
        }
        return false;
    }

    bool IsCollidingWithLightSource()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(_charPos);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("LightSource"))
            {
                return true;
            }
        }
        return false;
    }

    void MoveTowards(Vector2 target)
    {
        float velocity = _moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(_charPos, target, velocity);
    }


    void FixedUpdate() { }

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Button")
        {
            // Debug.Log("ACIONOU O BOTÃO!");
            Animator anim = collision.gameObject.GetComponent<Animator>();
            anim.Play("ButtonPress");
            GameObject door = GameObject.FindGameObjectWithTag("Door");
            GameObject doorOpen = GameObject.FindGameObjectWithTag("DoorOpen");
            door.SetActive(false);
            GetComponent<FearBar>().levelCtrl.PlaySound("Door");
            doorOpen.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (collision.gameObject.tag == "Secret")
        {
            // Debug.Log("ACIONOU O BOTÃO!");
            if (secretScreen != null)
            {
                GameObject.Find("PauseButton").SetActive(false);
                secretScreen.SetActive(true);
                GetComponent<FearBar>().levelCtrl.FreezeGame(true);
            }
            GetComponent<FearBar>().levelCtrl.PlaySound("Secret");
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Key")
        {
            // Debug.Log("ACIONOU O BOTÃO!");
            Key chave = collision.gameObject.GetComponent<Key>();
            ColetarChave(chave);
            GetComponent<FearBar>().levelCtrl.PlaySound("Secret");
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "KeyDoor")
        {
            KeyDoor porta = collision.gameObject.GetComponent<KeyDoor>();

            if (playerKeys.Contains(porta.tipoChaveNecessaria))
            {
                GetComponent<FearBar>().levelCtrl.PlaySound("Door");
                Animator anim = collision.gameObject.GetComponent<Animator>();
                anim.SetTrigger(porta.animacaoPorta.ToString());
                collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                //collision.gameObject.SetActive(false);

            }

        }
    }

    void ColetarChave(Key chave)
    {
        TipoChave tipoChave = chave.tipoChave;

        if (!playerKeys.Contains(tipoChave))
        {
            playerKeys.Add(tipoChave);
            ActivateChildWithTipoChave(tipoChave);
            Debug.Log("Chave " + tipoChave + " coletada!");
        }
    }


    private void ActivateChildWithTipoChave(TipoChave tipoChave)
    {
        // Encontra o GameObject pai com a tag "KeyList"
        GameObject keyList = GameObject.FindGameObjectWithTag("KeyList");

        if (keyList != null)
        {
            // Itera através dos filhos do GameObject pai
            foreach (Transform child in keyList.transform)
            {
                Key chave = child.GetComponent<Key>();
                if (chave.tipoChave == tipoChave)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }


    }
    // void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(_mousePos, _followRange);
    // }
}