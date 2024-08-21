using System;

namespace RTSPrototype.Core
{
    public interface ITickService
    {
        event Action OnTick;

        void RequestSpeedUpTime();
        void RequestSlowDownTime();
        void RequestStopResumeTime();

        void SetTickRate();
    }
}
