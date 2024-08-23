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
        private IAgentService agentServiceHandler;

        private void OnEnable()
        {
            agentServiceHandler.OnAgentSpawnRequested += AgentServiceHandler_OnAgentSpawnRequested;
            agentServiceHandler.OnAgentRemovalRequested += AgentServiceHandler_OnAgentRemovalRequested;
            agentServiceHandler.OnAllAgentsClearRequested += AgentServiceHandler_OnAllAgentsClearRequested;
        }

        private void OnDisable()
        {
            agentServiceHandler.OnAgentSpawnRequested -= AgentServiceHandler_OnAgentSpawnRequested;
            agentServiceHandler.OnAgentRemovalRequested -= AgentServiceHandler_OnAgentRemovalRequested;
            agentServiceHandler.OnAllAgentsClearRequested -= AgentServiceHandler_OnAllAgentsClearRequested;
        }

        private void AgentServiceHandler_OnAgentSpawnRequested()
        {
            var newAgent = Instantiate(agentPrefab, transform);
            newAgent.Initialize();

            newAgent.OnDestinationReached += NewAgent_OnDestinationReached;

            agents.Add(newAgent);

            agentServiceHandler.RegisterAgent(agents.Count);
        }

        private void AgentServiceHandler_OnAgentRemovalRequested()
        {
            var agentToRemove = agents[Random.Range(0, agents.Count)];

            agentToRemove.OnDestinationReached -= NewAgent_OnDestinationReached;

            agents.Remove(agentToRemove);
            agentToRemove.Remove();

            agentServiceHandler.UnregisterAgent(agents.Count);
        }

        private void AgentServiceHandler_OnAllAgentsClearRequested()
        {
            for (int i = agents.Count - 1; i >= 0; i--)
            {
                agents[i].Remove();
                agents.RemoveAt(i);

                agentServiceHandler.UnregisterAgent(agents.Count);
            }
        }

        private void NewAgent_OnDestinationReached(string agentGuid)
        {
            agentServiceHandler.TriggerDestinationReached(agentGuid);
        }
    }
}
