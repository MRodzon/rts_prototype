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
        private UIGameSpeedLabel uiGameSpeedLabel;
        [SerializeField]
        private UIButton uiSpeedUpTimeButton;
        [SerializeField]
        private UIButton uiSlowDownTimeButton;
        [SerializeField]
        private UIButton uiStopResumeTimeButton;

        [Header("Other")]
        [SerializeField]
        private UIInfoLabel uiInfoLabel;

        [Inject]
        private IAgentService agentService;
        [Inject]
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
            agentService.OnDestinationReached += AgentService_OnDestinationReached;

            tickService.OnGameSpeedChanged += TickService_OnGameSpeedChanged;
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
            agentService.OnDestinationReached -= AgentService_OnDestinationReached;

            tickService.OnGameSpeedChanged -= TickService_OnGameSpeedChanged;
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

        private void AgentService_OnDestinationReached(string agentGuid)
        {
            uiInfoLabel.TriggerInfoMessage(agentGuid);
        }

        private void TickService_OnGameSpeedChanged(int value)
        {
            uiGameSpeedLabel.UpdateLabel(value);
        }
    }
}
