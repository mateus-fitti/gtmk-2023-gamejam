using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour
{
    public Transform targetCamera;
    private RectTransform canvasRectTransform;

    private void Awake()
    {
        canvasRectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        canvasRectTransform.position = targetCamera.position;
    }
}
