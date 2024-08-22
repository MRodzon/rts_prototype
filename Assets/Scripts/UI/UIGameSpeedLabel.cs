using TMPro;
using UnityEngine;

namespace RTSPrototype.UI
{
    public class UIGameSpeedLabel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text labelText;

        private void Awake()
        {
            labelText.text = "0";
        }

        public void UpdateLabel(int value)
        {
            labelText.text = value.ToString();
        }
    }
}
