using DG.Tweening;
using TMPro;
using UnityEngine;

namespace RTSPrototype.UI
{
    public class UIInfoLabel : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup labelCanvasGroup;
        [SerializeField]
        private TMP_Text infoText;

        [SerializeField]
        private float labelFadeTransitionTime;
        [SerializeField]
        private float labelVisibilityTime;

        private Sequence sequence;

        private const string DESTINATION_REACHED_MESSAGE = "Agent {0} arrived.";

        public void TriggerInfoMessage(string content)
        {
            infoText.text = string.Format(DESTINATION_REACHED_MESSAGE, content);

            sequence.Complete();
            sequence = DOTween.Sequence();
            sequence.Append(labelCanvasGroup.DOFade(1f, labelFadeTransitionTime));
            sequence.AppendInterval(labelVisibilityTime);
            sequence.Append(labelCanvasGroup.DOFade(0f, labelFadeTransitionTime));
        }
    }
}
