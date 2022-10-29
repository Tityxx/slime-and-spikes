using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public static event Action onPointerDown = delegate { };
    public static event Action onPointerUp = delegate { };

    public UnityEvent OnDown;
    public UnityEvent OnUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDown.Invoke();
        onPointerDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnUp.Invoke();
        onPointerUp();
    }
}