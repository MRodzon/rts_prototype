using System;

namespace RTSPrototype.Core
{
    public interface IAgentService
    {
        event Action<int> OnAgentCountUpdated;

        void RequestAgentSpawn();
        void RequestAgentRemoval();
        void RequestAllAgentsRemoval();

        void RegisterAgent();
    }
}
