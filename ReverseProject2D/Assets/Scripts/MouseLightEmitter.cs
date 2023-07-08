using UnityEngine;

public class MouseLightEmitter : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject circleLight;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -mainCamera.transform.position.z;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        circleLight.transform.position = worldPosition;
        
    }
}
