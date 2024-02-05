using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class RectTransformZoomer : MonoBehaviour, IDragHandler
{
    [SerializeField] Canvas uiCanvas;
    [SerializeField] float zoomSpeed = 1f;
    [SerializeField] float minZoomRate = 1f;
    [SerializeField] float maxZoomRate = 10f;

    RectTransform targetContent;
    CanvasScaler canvasScaler;

    float CurrentZoomScale => targetContent.localScale.x;

    bool ShouldScaleDragMove =>
        canvasScaler != null &&
        canvasScaler.IsActive() &&
        canvasScaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize;

    void Awake()
    {
        targetContent = GetComponent<RectTransform>();
        canvasScaler = uiCanvas.GetComponent<CanvasScaler>();
    }

    void Update()
    {
        var scroll = Input.mouseScrollDelta.y;
        ScrollToZoomMap(Input.mousePosition, scroll);
    }

    /// <summary>
    /// マウスの位置調整
    /// </summary>
    /// <param name="mousePosition"></param>
    /// <param name="scroll"></param>
    public void ScrollToZoomMap(Vector2 mousePosition, float scroll)
    {
        GetLocalPointInRectangle(mousePosition, out var beforeZoomLocalPosition);

        var afterZoomScale = CurrentZoomScale + scroll * zoomSpeed;
        afterZoomScale = Mathf.Clamp(afterZoomScale, minZoomRate, maxZoomRate);
        DoZoom(afterZoomScale);

        GetLocalPointInRectangle(mousePosition, out var afterZoomLocalPosition);

        var positionDiff = afterZoomLocalPosition - beforeZoomLocalPosition;
        var scaledPositionDiff = positionDiff * afterZoomScale;
        var newAnchoredPosition = targetContent.anchoredPosition + scaledPositionDiff;

        targetContent.anchoredPosition = newAnchoredPosition;
    }

    /// <summary>
    /// スクロール量によってズーム
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        var dragMoveDelta = eventData.delta;

        if (ShouldScaleDragMove)
        {
            var dragMoveScale = canvasScaler.referenceResolution.x / Screen.width;
            dragMoveDelta *= dragMoveScale;
        }

        targetContent.anchoredPosition += dragMoveDelta;
    }

    void DoZoom(float zoomScale)
    {
        targetContent.localScale = Vector3.one * zoomScale;
    }

    void GetLocalPointInRectangle(Vector2 mousePosition, out Vector2 localPosition)
    {
        var targetCamera = uiCanvas.renderMode switch
        {
            RenderMode.ScreenSpaceCamera => uiCanvas.worldCamera,
            RenderMode.ScreenSpaceOverlay => null,
            _ => throw new System.NotSupportedException(),
        };

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            targetContent, mousePosition, targetCamera, out localPosition);
    }
}