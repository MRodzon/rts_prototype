using System;

namespace RTSPrototype.Core
{
    public class TickServiceHandler : ITickService
    {
        public event Action OnTick;

        public void SetTickRate()
        {

        }
    }
}
