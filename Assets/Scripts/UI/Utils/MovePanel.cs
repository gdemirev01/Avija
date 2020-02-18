using UnityEngine;
using UnityEngine.EventSystems;

public class MovePanel : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    private RectTransform dragRectTransform;
    private Canvas canvas;

    private UIController uIController;

    void Start()
    {
        dragRectTransform = this.GetComponent<RectTransform>();
        canvas = transform.root.gameObject.GetComponent<Canvas>();

        uIController = UIController.Instance;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragRectTransform.SetAsLastSibling();
        uIController.SetLastPanel(this.gameObject);
    }
}
