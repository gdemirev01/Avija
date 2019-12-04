using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovePanel : MonoBehaviour, IDragHandler, IPointerDownHandler
{

    private RectTransform dragRectTransform;
    private Canvas canvas;

    void Start()
    {
        dragRectTransform = this.GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragRectTransform.SetAsLastSibling();
    }
}
