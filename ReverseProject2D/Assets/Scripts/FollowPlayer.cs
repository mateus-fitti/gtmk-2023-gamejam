using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float smoothSpeed = 5f; // Velocidade de suavização do movimento do personagem

    private Vector3 targetPosition;

    private PolygonCollider2D mapBounds;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    private bool isMobile = false; // Flag para verificar se estamos em um dispositivo móvel

    private void Start()
    {
        transform.position = new Vector3(-20.5f, -10.5900002f, 0);
        if (mapBounds)
        {
            // Assuming you have a reference to the map bounds object with PolygonCollider2D attached
            mapBounds = GameObject.Find("MapBounds").GetComponent<PolygonCollider2D>();

            // Calculate the minimum and maximum bounds of the map
            CalculateMapBounds();
        }

        // Verifica se estamos em um dispositivo móvel
        isMobile = Application.isMobilePlatform;
    }

    private void CalculateMapBounds()
    {
        // Get the points that make up the polygon collider's path
        Vector2[] path = mapBounds.GetPath(0);

        // Set the initial min and max values to the first point
        minBounds = path[0];
        maxBounds = path[0];

        // Iterate over the path points to find the minimum and maximum values
        for (int i = 1; i < path.Length; i++)
        {
            minBounds = Vector2.Min(minBounds, path[i]);
            maxBounds = Vector2.Max(maxBounds, path[i]);
        }
    }

    private void Update()
    {
        if (mapBounds)
        {
            if (isMobile)
            {
                if (Input.touchCount > 0)
                {
                    // Use o primeiro toque para definir a posição alvo
                    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                    // Clamp a posição do toque dentro dos limites do mapa
                    Vector2 clampedPosition = new Vector2(
                        Mathf.Clamp(touchPosition.x, minBounds.x, maxBounds.x),
                        Mathf.Clamp(touchPosition.y, minBounds.y, maxBounds.y)
                    );

                    targetPosition = clampedPosition;
                }
            }
            else
            {
                // Para plataformas não móveis, mantenha o comportamento original com o mouse
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Clamp a posição do mouse dentro dos limites do mapa
                Vector2 clampedPosition = new Vector2(
                    Mathf.Clamp(mousePosition.x, minBounds.x, maxBounds.x),
                    Mathf.Clamp(mousePosition.y, minBounds.y, maxBounds.y)
                );

                targetPosition = clampedPosition;
            }
        }
        else
        {
            // Mantenha o comportamento original para quando não houver limites de mapa definidos
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorPosition.z = 0f; // Mantém a posição do cursor no plano 2D
            targetPosition = cursorPosition;
        }
    }

    private void FixedUpdate()
    {
        // Move o personagem suavemente em direção à posição alvo
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
