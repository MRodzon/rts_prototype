using System;

namespace RTSPrototype.Core
{
    public interface IAgentService
    {
        event Action<int> OnAgentCountUpdated;

        event Action OnAgentSpawnRequested;
        event Action OnAgentRemovalRequested;
        event Action OnAllAgentsClearRequested;

        event Action<string> OnDestinationReached;

        void RequestAgentSpawn();
        void RequestAgentRemoval();
        void RequestAllAgentsRemoval();

        void RegisterAgent(int currentAgentsCountValue);
        void UnregisterAgent(int currentAgentsCountValue);

        void TriggerDestinationReached(string agentGuid);
    }
}
