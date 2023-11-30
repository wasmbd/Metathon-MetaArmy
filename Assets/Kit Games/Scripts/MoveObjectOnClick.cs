using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveObjectWithMouse : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Button moveButton;
    public Transform objectToMove;

    private bool isDragging = false;
    private Vector2 offset;

    private void Start()
    {
        moveButton.onClick.AddListener(ToggleDragging);
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 canvasPos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                objectToMove.parent.GetComponent<RectTransform>(),
                mousePos,
                null,
                out canvasPos
            );

            objectToMove.localPosition = canvasPos + offset;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 mousePos = eventData.position;
        Vector2 objectPos = objectToMove.position;

        offset = objectPos - mousePos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

    private void ToggleDragging()
    {
        isDragging = !isDragging;
    }
    
    public void RemoveObject()
    {
        Destroy(gameObject);
    }
}
