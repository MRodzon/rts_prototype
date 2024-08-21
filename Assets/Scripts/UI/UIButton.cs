using System;
using UnityEngine;
using UnityEngine.UI;

namespace RTSPrototype.UI
{
    public class UIButton : UIElement
    {
        public event Action OnButtonClicked;

        [SerializeField]
        private Button button;

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
            OnButtonClicked?.Invoke();
        }
    }
}
