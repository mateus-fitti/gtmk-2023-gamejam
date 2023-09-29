using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform patrolPointsParent;
    public float moveSpeed = 3.0f;
    public float detectionRange = 5.0f;
    public float maxChaseDistance = 10.0f; // Distância máxima para perseguir
    public float patrolDelay = 1.0f; // Delay de patrulha em segundos
    public string targetTag = "Abigail";

    private Transform[] patrolPoints;
    private int currentPatrolIndex;
    private Transform target;
    private Vector3 initialPosition;
    private bool isChasing;
    private float patrolTimer;

    private FearBar fearBar;


    private void Start()
    {
        // Obtenha os pontos de patrulha a partir do GameObject "patrolPointsParent"
        patrolPoints = patrolPointsParent.GetComponentsInChildren<Transform>();

        // Ignore o transform pai (o próprio "patrolPointsParent")
        currentPatrolIndex = 1;

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

            if (isChasing)
            {
                this.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;

                // Se o inimigo estiver perseguindo o alvo, verifique a distância de perseguição máxima
                if (distanceToTarget > maxChaseDistance || fearBar.GetSafeZone())
                {
                    isChasing = false;
                }
                else
                {
                    // Continue perseguindo o alvo
                    transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                }
            }
            else
            {
                // Se o alvo estiver dentro do alcance de detecção, persiga-o
                if (distanceToTarget <= detectionRange)
                {
                    isChasing = true;
                }
                else
                {
                    // Se não estiver perseguindo, continue a patrulha
                    Patrol();
                }
            }
        }
        else
        {
            // Se o alvo não estiver presente, continue a patrulha
            Patrol();
        }
    }

    private void Patrol()
    {
        this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;

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
}
