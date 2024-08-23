using System;

namespace RTSPrototype.Core
{
    public class TickServiceHandler : ITickService
    {
        public event Action<int> OnGameSpeedChanged;
        public event Action OnTick;

        private int gameSpeed;
        private bool isGameStopped;

        public void SetTickRate()
        {

        }

        public void RequestSlowDownTime()
        {
            if (gameSpeed == 0)
            {
                return;
            }

            gameSpeed--;
            isGameStopped = false;

            OnGameSpeedChanged?.Invoke(gameSpeed);
        }

        public void RequestSpeedUpTime()
        {
            gameSpeed++;
            isGameStopped = false;

            OnGameSpeedChanged?.Invoke(gameSpeed);
        }

        public void RequestStopResumeTime()
        {
            isGameStopped = !isGameStopped;
            OnGameSpeedChanged?.Invoke(isGameStopped ? 0 : gameSpeed);
        }

        public bool IsGameStopped()
        {
            return gameSpeed == 0 || isGameStopped;
        }
    }
}
