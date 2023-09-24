using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class SpinnerInput : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject spinButtonObject;

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
        spinButtonObject.SetActive(active);
    }
}