using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float smoothSpeed = 5f; // Velocidade de suavização do movimento do personagem

    private Vector3 targetPosition;

    private void Update()
    {
        // Obtém a posição do cursor na tela
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0f; // Mantém a posição do cursor no plano 2D

        // Define a posição alvo como a posição do cursor
        targetPosition = cursorPosition;
    }

    private void FixedUpdate()
    {
        // Move o personagem suavemente em direção à posição alvo
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }

}
