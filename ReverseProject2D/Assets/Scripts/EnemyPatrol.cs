using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform patrolPointsParent;
    public float moveSpeed = 3.0f;
    public float detectionRange = 5.0f;
    public float maxChaseDistance = 10.0f; // Distância máxima para perseguir
    public float patrolDelay = 1.0f; // Delay de patrulha em segundos
    public string targetTag = "Abigail";

    public bool patrolEnabled = false;

    private Transform[] patrolPoints;
    private int currentPatrolIndex;
    private Transform target;
    private Vector3 initialPosition;
    private bool isChasing;
    private float patrolTimer;
    private bool chaseSoundFlag = false;

    private FearBar fearBar;


    private void Start()
    {
        // Obtenha os pontos de patrulha a partir do GameObject "patrolPointsParent"
        if (patrolPointsParent) patrolPoints = patrolPointsParent.GetComponentsInChildren<Transform>();

        // Ignore o transform pai (o próprio "patrolPointsParent")
        currentPatrolIndex = 0;

        // Defina a posição inicial do inimigo
        initialPosition = transform.position;

        // Inicialize o timer de patrulha para que o inimigo comece a se mover imediatamente
        patrolTimer = 0;

        // Encontre o GameObject com a tag "Abigail" e armazene seu transform
        GameObject abigail = GameObject.FindGameObjectWithTag(targetTag);
        if (abigail != null)
        {
            target = abigail.transform;
            fearBar = abigail.GetComponent<FearBar>();
        }

    }

    private void Update()
    {
        if (target != null)
        {
            // Verifique a distância entre o inimigo e o alvo
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            
            if (distanceToTarget <= detectionRange)
                isChasing = true;
            else if (isChasing && distanceToTarget > maxChaseDistance || fearBar.GetSafeZone())
            {
                isChasing = false;
            }
            
             if (isChasing)
            {
                this.GetComponent<CircleCollider2D>().isTrigger = false;
                PlayChaseSound();
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                this.GetComponent<CircleCollider2D>().isTrigger = true;
                Patrol();
            }
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        StopChaseSound();

        if (patrolEnabled)
        {
            // Verifique se chegou ao ponto de patrulha atual
            if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
            {
                // Aguarde o tempo de patrulha antes de mover para o próximo ponto
                if (patrolTimer <= 0)
                {
                    // Mova-se para o próximo ponto de patrulha
                    currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                    patrolTimer = patrolDelay; // Configure o timer de patrulha para o próximo atraso
                }
                else
                {
                    patrolTimer -= Time.deltaTime;
                }
            }

            // Mova-se em direção ao ponto de patrulha atual
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
        }

    }

    void PlayChaseSound(){
        if(!chaseSoundFlag){
        SoundManager.instance.Play("Monster_Chase");
        chaseSoundFlag = true;
        } 

    }

    void StopChaseSound(){
        if(chaseSoundFlag){
        SoundManager.instance.Stop("Monster_Chase");
        chaseSoundFlag = false;
        }
    }
}
