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

            /*agentsCount++;
            OnAgentCountUpdated?.Invoke(agentsCount);*/
        }

        public void RequestAgentRemoval()
        {
            OnAgentRemovalRequested?.Invoke();

            /*if(agentsCount == 0)
            {
                return;
            }

            agentsCount--;
            OnAgentCountUpdated?.Invoke(agentsCount);*/
        }

        public void RequestAllAgentsRemoval()
        {
            OnAllAgentsClearRequested?.Invoke();

            /*agentsCount = 0;
            OnAgentCountUpdated?.Invoke(agentsCount);*/
        }

        public void TriggerDestinationReached(string agentGuid)
        {
            OnDestinationReached?.Invoke(agentGuid);
        }
    }
}
