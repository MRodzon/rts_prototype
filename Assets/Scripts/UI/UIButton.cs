using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RTSPrototype.UI
{
    public class UIButton : UIElement, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action OnButtonClicked;

        [SerializeField]
        private Button button;

        [SerializeField]
        private float selectionTime;
        [SerializeField]
        private float selectionScale;

        [SerializeField]
        private float clickTime;
        [SerializeField]
        private float clickScale;

        private Sequence sequence;

        private void OnEnable()
        {
            button.onClick.AddListener(Button_OnClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(Button_OnClick);
        }

        private void Button_OnClick()
        {
            transform.DOComplete();
            transform.DOPunchScale(-Vector3.one * clickScale, clickTime);

            OnButtonClicked?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(selectionScale, selectionTime).SetEase(Ease.InCubic);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1f, selectionTime).SetEase(Ease.OutCubic);
        }
    }
}
