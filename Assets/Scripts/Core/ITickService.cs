using System;

namespace RTSPrototype.Core
{
    public interface ITickService
    {
        event Action<int> OnGameSpeedChanged;
        event Action OnTick;

        void RequestSpeedUpTime();
        void RequestSlowDownTime();
        void RequestStopResumeTime();

        bool IsGameStopped();

        void SetTickRate();
    }
}
