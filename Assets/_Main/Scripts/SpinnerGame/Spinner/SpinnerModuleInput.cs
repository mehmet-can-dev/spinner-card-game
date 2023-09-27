using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpinnerGame.Spinner
{


    [RequireComponent(typeof(Graphic))]
    public class SpinnerModuleInput : MonoBehaviour, IPointerClickHandler
    {
        private Action _onClick;
        private bool isActive;
        
        [Header("Child References")] [SerializeField]
        private GameObject spinButtonObject;
        
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
}