using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform target;
    private Transform homePos;
    private Transform ghost;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float range;
    [SerializeField]
    private float safeRange;
    private void Start()
    {
        homePos = transform.GetChild(0);
        target = FindObjectOfType<CharacterMovement>().transform;
        ghost = FindObjectOfType<FollowPlayer>().transform;
        homePos.parent = null;

    }

    private void Update()
    {

        // if (Vector3.Distance(target.position, transform.position) <= range && Vector3.Distance(target.position, transform.position) > 0 && Vector3.Distance(ghost.position, target.position) > safeRange)
        // {
        //     followPlayer();
        // }
        // else
        // {
        //     goHome();
        // }
    }

    public void followPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void goHome()
    {
        transform.position = Vector3.MoveTowards(transform.position, homePos.transform.position, speed * Time.deltaTime);

    }
}