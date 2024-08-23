using System;

namespace RTSPrototype.Core
{
    public class AgentServiceHandler : IAgentService
    {
        public event Action<int> OnAgentCountUpdated;

        public event Action OnAgentSpawnRequested;
        public event Action OnAgentRemovalRequested;
        public event Action OnAllAgentsClearRequested;

        public event Action<string> OnDestinationReached;

        public void RegisterAgent(int currentAgentsCountValue)
        {
            OnAgentCountUpdated?.Invoke(currentAgentsCountValue);
        }

        public void UnregisterAgent(int currentAgentsCountValue)
        {
            OnAgentCountUpdated?.Invoke(currentAgentsCountValue);
        }

        public void RequestAgentSpawn()
        {
            OnAgentSpawnRequested?.Invoke();
        }

        public void RequestAgentRemoval()
        {
            OnAgentRemovalRequested?.Invoke();
        }

        public void RequestAllAgentsRemoval()
        {
            OnAllAgentsClearRequested?.Invoke();
        }

        public void TriggerDestinationReached(string agentGuid)
        {
            OnDestinationReached?.Invoke(agentGuid);
        }
    }
}
