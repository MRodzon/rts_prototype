using System;

namespace RTSPrototype.Core
{
    public interface ITickService
    {
        event Action<int> OnGameSpeedChanged;

        void SpeedUpTime();
        void SlowDownTime();
        void StopResumeTime();

        bool IsGameStopped();
        int GetGameSpeed();
    }
}
