using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PedalButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public event Action<bool> OnTouch;

    public void OnDrag(PointerEventData eventData)
    {
        OnTouch?.Invoke(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTouch?.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnTouch?.Invoke(false);
    }
}
