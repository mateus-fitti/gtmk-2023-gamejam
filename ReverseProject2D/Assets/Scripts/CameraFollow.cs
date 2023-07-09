using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referência ao objeto que a câmera deve seguir
    public float smoothSpeed = 0.125f; // Velocidade suave de movimento da câmera

    private Vector3 offset; // Distância entre a câmera e o objeto

    void Start()
    {
        offset = transform.position - target.position; // Calcula a distância inicial entre a câmera e o objeto
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // Calcula a posição desejada da câmera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Suaviza o movimento da câmera
        transform.position = smoothedPosition; // Atualiza a posição da câmera
    }
}
