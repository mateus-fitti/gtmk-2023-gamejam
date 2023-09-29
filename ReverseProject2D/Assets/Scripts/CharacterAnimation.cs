using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    private Transform targetObject; // O objeto que você deseja seguir

    void Start()
    {
        targetObject = FindObjectOfType<FollowPlayer>().transform;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (targetObject != null)
        {
            // Calcula o vetor direção do personagem para o objeto de destino
            Vector3 directionToTarget = targetObject.position - transform.position;

            // Calcula o ângulo entre o vetor de direção e a direção para cima (vetor Y)
            float angle = Vector3.Angle(Vector3.up, directionToTarget);

            // Calcula o produto vetorial entre os vetores para determinar o lado
            Vector3 crossProduct = Vector3.Cross(Vector3.up, directionToTarget);
            if (crossProduct.z < 0)
            {
                angle = 360 - angle; // Inverte o ângulo para o lado esquerdo
            }

            // Define as animações com base no ângulo calculado
            if (angle >= 45 && angle < 135)
            {
                // Olhando para esquerda
                animator.Play("Aurora_Left");
            }
            else if (angle >= 135 && angle < 225)
            {
                // Olhando para baixo
                animator.Play("Aurora_Down");
            }
            else if (angle >= 225 && angle < 315)
            {
                // Olhando para direita
                animator.Play("Aurora_Right");
            }
            else
            {
                // Olhando para cima
                animator.Play("Aurora_Up");
            }
        }
    }

}
