using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public static event Action onPointerDown = delegate { };
    public static event Action onPointerUp = delegate { };

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onPointerUp();
    }
}