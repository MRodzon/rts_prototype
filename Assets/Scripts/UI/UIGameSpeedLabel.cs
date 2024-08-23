using TMPro;
using UnityEngine;

namespace RTSPrototype.UI
{
    public class UIGameSpeedLabel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text labelText;

        public void Initialize(int value)
        {
            labelText.text = value.ToString();
        }

        public void UpdateLabel(int value)
        {
            labelText.text = value.ToString();
        }
    }
}
