using RTSPrototype.Core;
using UnityEngine;
using Zenject;

namespace RTSPrototype.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Agents")]
        [SerializeField]
        private UIAgentAmountLabel uiAgentAmountLabel;
        [SerializeField]
        private UIButton uiSpawnAgentButton;
        [SerializeField]
        private UIButton uiRemoveAgentButton;
        [SerializeField]
        private UIButton uiRemoveAllAgentsButton;

        [Header("Time")]
        [SerializeField]
        private UIButton uiSpeedUpTimeButton;
        [SerializeField]
        private UIButton uiSlowDownTimeButton;
        [SerializeField]
        private UIButton uiStopResumeTimeButton;

        private IAgentService agentService;
        private ITickService tickService;

        private void OnEnable()
        {
            uiSpawnAgentButton.OnButtonClicked += UiSpawnAgentButton_OnButtonClicked;
            uiRemoveAgentButton.OnButtonClicked += UiRemoveAgentButton_OnButtonClicked;
            uiRemoveAllAgentsButton.OnButtonClicked += UiRemoveAllAgentsButton_OnButtonClicked;

            uiSpeedUpTimeButton.OnButtonClicked += UiSpeedUpTimeButton_OnButtonClicked;
            uiSlowDownTimeButton.OnButtonClicked += UiSlowDownTimeButton_OnButtonClicked;
            uiStopResumeTimeButton.OnButtonClicked += UiStopResumeTimeButton_OnButtonClicked;

            agentService.OnAgentCountUpdated += AgentService_OnAgentCountUpdated;
        }

        private void OnDisable()
        {
            uiSpawnAgentButton.OnButtonClicked -= UiSpawnAgentButton_OnButtonClicked;
            uiRemoveAgentButton.OnButtonClicked -= UiRemoveAgentButton_OnButtonClicked;
            uiRemoveAllAgentsButton.OnButtonClicked -= UiRemoveAllAgentsButton_OnButtonClicked;

            uiSpeedUpTimeButton.OnButtonClicked -= UiSpeedUpTimeButton_OnButtonClicked;
            uiSlowDownTimeButton.OnButtonClicked -= UiSlowDownTimeButton_OnButtonClicked;
            uiStopResumeTimeButton.OnButtonClicked -= UiStopResumeTimeButton_OnButtonClicked;

            agentService.OnAgentCountUpdated -= AgentService_OnAgentCountUpdated;
        }

        [Inject]
        private void Inject(IAgentService agentService, ITickService tickService)
        {
            this.agentService = agentService;
            this.tickService = tickService;
        }

        private void UiSpawnAgentButton_OnButtonClicked()
        {
            agentService.RequestAgentSpawn();
        }

        private void UiRemoveAgentButton_OnButtonClicked()
        {
            agentService.RequestAgentRemoval();
        }

        private void UiRemoveAllAgentsButton_OnButtonClicked()
        {
            agentService.RequestAllAgentsRemoval();
        }

        private void UiSpeedUpTimeButton_OnButtonClicked()
        {
            tickService.RequestSpeedUpTime();
        }

        private void UiSlowDownTimeButton_OnButtonClicked()
        {
            tickService.RequestSlowDownTime();
        }

        private void UiStopResumeTimeButton_OnButtonClicked()
        {
            tickService.RequestStopResumeTime();
        }

        private void AgentService_OnAgentCountUpdated(int value)
        {
            uiAgentAmountLabel.UpdateLabel(value);
        }
    }
}
