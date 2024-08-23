using RTSPrototype.Core;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RTSPrototype.Agents
{
    public class AgentManager : MonoBehaviour
    {
        [SerializeField]
        private Agent agentPrefab;

        private List<Agent> agents = new();

        [Inject]
        private IAgentService agentService;
        [Inject]
        private ITickService tickService;

        private void OnEnable()
        {
            agentService.OnAgentSpawnRequested += AgentServiceHandler_OnAgentSpawnRequested;
            agentService.OnAgentRemovalRequested += AgentServiceHandler_OnAgentRemovalRequested;
            agentService.OnAllAgentsClearRequested += AgentServiceHandler_OnAllAgentsClearRequested;

            tickService.OnGameSpeedChanged += TickService_OnGameSpeedChanged;
        }

        private void OnDisable()
        {
            agentService.OnAgentSpawnRequested -= AgentServiceHandler_OnAgentSpawnRequested;
            agentService.OnAgentRemovalRequested -= AgentServiceHandler_OnAgentRemovalRequested;
            agentService.OnAllAgentsClearRequested -= AgentServiceHandler_OnAllAgentsClearRequested;

            tickService.OnGameSpeedChanged -= TickService_OnGameSpeedChanged;
        }

        private void AgentServiceHandler_OnAgentSpawnRequested()
        {
            var newAgent = Instantiate(agentPrefab, transform);
            newAgent.Initialize(tickService.GetGameSpeed());

            newAgent.OnDestinationReached += NewAgent_OnDestinationReached;

            agents.Add(newAgent);

            agentService.RegisterAgent(agents.Count);
        }

        private void AgentServiceHandler_OnAgentRemovalRequested()
        {
            var agentToRemove = agents[Random.Range(0, agents.Count)];

            agentToRemove.OnDestinationReached -= NewAgent_OnDestinationReached;

            agents.Remove(agentToRemove);
            agentToRemove.Remove();

            agentService.UnregisterAgent(agents.Count);
        }

        private void AgentServiceHandler_OnAllAgentsClearRequested()
        {
            for (int i = agents.Count - 1; i >= 0; i--)
            {
                agents[i].Remove();
                agents.RemoveAt(i);

                agentService.UnregisterAgent(agents.Count);
            }
        }

        private void TickService_OnGameSpeedChanged(int value)
        {
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].SetTimeModifier(value);
            }
        }

        private void NewAgent_OnDestinationReached(string agentGuid)
        {
            agentService.TriggerDestinationReached(agentGuid);
        }
    }
}
