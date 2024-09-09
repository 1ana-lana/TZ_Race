using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class СarSteeringWheel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action<bool, Vector2> OnTouch;
    [SerializeField]
    private RectTransform _buttonRectTransform;

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 touchPosition = eventData.position;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_buttonRectTransform, touchPosition, eventData.pressEventCamera, out localPoint);
        OnTouch?.Invoke(true, localPoint);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnTouch?.Invoke(false, new Vector2());
    }
}

