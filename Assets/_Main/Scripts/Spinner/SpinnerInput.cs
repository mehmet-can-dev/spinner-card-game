using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpinnerInput : MonoBehaviour, IPointerClickHandler
{
    private Action _onClick;

    private bool isActive;

    public void Init(Action OnClick)
    {
        _onClick = OnClick;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isActive)
            return;

        _onClick?.Invoke();
    }

    public void SetActive(bool active)
    {
        isActive = active;
    }
}