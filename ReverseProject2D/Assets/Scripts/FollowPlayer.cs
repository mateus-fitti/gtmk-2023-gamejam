using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float smoothSpeed = 5f; // Velocidade de suavização do movimento do personagem

    private Vector3 targetPosition;

    private PolygonCollider2D mapBounds;
    private Vector2 canvasBoundsMin;
    private Vector2 canvasBoundsMax;
    private bool isMobile = false; // Flag para verificar se estamos em um dispositivo móvel

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Abigail")) transform.position = GameObject.FindGameObjectWithTag("Abigail").transform.position;
        Cursor.visible = false;

        // Tente encontrar o objeto "MapBounds" na cena
        GameObject mapBoundsObject = GameObject.Find("MapBounds");

        if (mapBoundsObject != null)
        {
            // Se o objeto for encontrado, obtenha o componente PolygonCollider2D
            mapBounds = mapBoundsObject.GetComponent<PolygonCollider2D>();

            if (mapBounds != null)
            {
                // Se o componente for encontrado, calcule os limites do mapa
                CalculateMapBounds();
            }
            else
            {
                // Se o componente não for encontrado, imprima uma mensagem de erro
                //Debug.LogError("PolygonCollider2D component not found on MapBounds object.");
            }
        }
        else
        {
            // Se o objeto "MapBounds" não for encontrado, calcule os limites do canvas
            CalculateCanvasBounds();
        }

        // Verifique se estamos em um dispositivo móvel
        isMobile = Application.isMobilePlatform;
    }

    private void CalculateMapBounds()
    {
        Vector2[] path = mapBounds.GetPath(0);

        Vector2 minBounds = path[0];
        Vector2 maxBounds = path[0];

        for (int i = 1; i < path.Length; i++)
        {
            minBounds = Vector2.Min(minBounds, path[i]);
            maxBounds = Vector2.Max(maxBounds, path[i]);
        }

        // Defina os limites com base nos limites do mapa
        canvasBoundsMin = minBounds;
        canvasBoundsMax = maxBounds;
    }

    private void CalculateCanvasBounds()
    {
        // Obtenha o tamanho do canvas em cena
        Canvas canvas = FindObjectOfType<Canvas>();
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 canvasSize = canvasRect.sizeDelta;

        // Calcule os limites do canvas com base em seu tamanho
        canvasBoundsMin = -canvasSize / 2;
        canvasBoundsMax = canvasSize / 2;
    }

    private void Update()
    {
        if (isMobile)
        {
            if (Input.touchCount > 0)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                targetPosition = touchPosition;
            }
        }
        else
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = mousePosition;
        }

        // Clamp a posição do personagem dentro dos limites calculados
        targetPosition.x = Mathf.Clamp(targetPosition.x, canvasBoundsMin.x, canvasBoundsMax.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, canvasBoundsMin.y, canvasBoundsMax.y);
    }

    private void FixedUpdate()
    {
        // Move o personagem suavemente em direção à posição alvo
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
