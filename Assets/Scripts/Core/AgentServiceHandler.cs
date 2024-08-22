using System;

namespace RTSPrototype.Core
{
    public class AgentServiceHandler : IAgentService
    {
        public event Action<int> OnAgentCountUpdated;

        private int agentsCount;

        public void RegisterAgent()
        {

        }

        public void UnregisterAgent()
        {

        }

        public void RequestAgentSpawn()
        {
            agentsCount++;
            OnAgentCountUpdated?.Invoke(agentsCount);
        }

        public void RequestAgentRemoval()
        {
            if(agentsCount == 0)
            {
                return;
            }

            agentsCount--;
            OnAgentCountUpdated?.Invoke(agentsCount);
        }

        public void RequestAllAgentsRemoval()
        {
            agentsCount = 0;
            OnAgentCountUpdated?.Invoke(agentsCount);
        }
    }
}
