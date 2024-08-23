using System;

namespace RTSPrototype.Core
{
    public class TickServiceHandler : ITickService
    {
        public event Action<int> OnGameSpeedChanged;

        private int gameSpeed = 1;
        private bool isGameStopped;

        public void SlowDownTime()
        {
            if (gameSpeed == 0)
            {
                return;
            }

            gameSpeed--;
            isGameStopped = false;

            OnGameSpeedChanged?.Invoke(gameSpeed);
        }

        public void SpeedUpTime()
        {
            gameSpeed++;
            isGameStopped = false;

            OnGameSpeedChanged?.Invoke(gameSpeed);
        }

        public void StopResumeTime()
        {
            isGameStopped = !isGameStopped;
            OnGameSpeedChanged?.Invoke(isGameStopped ? 0 : gameSpeed);
        }

        public bool IsGameStopped()
        {
            return gameSpeed == 0 || isGameStopped;
        }

        public int GetGameSpeed()
        {
            return isGameStopped ? 0 : gameSpeed;
        }
    }
}
