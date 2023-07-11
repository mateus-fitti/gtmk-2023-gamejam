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

    private void Start()
    {
        // Assuming you have a reference to the map bounds object with PolygonCollider2D attached
        mapBounds = GameObject.Find("MapBounds").GetComponent<PolygonCollider2D>();

        // Calculate the minimum and maximum bounds of the map
        CalculateMapBounds();
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
       Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Clamp the mouse position within the map bounds
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(mousePosition.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(mousePosition.y, minBounds.y, maxBounds.y)
        );

        // Move the character to the clamped position
        transform.position = clampedPosition;
    }

    private void FixedUpdate()
    {
        // Move o personagem suavemente em direção à posição alvo
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }

}