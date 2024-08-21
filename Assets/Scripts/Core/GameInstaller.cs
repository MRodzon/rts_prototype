using Zenject;

namespace RTSPrototype.Core
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAgentService>().To<AgentServiceHandler>().AsSingle();
            Container.Bind<ITickService>().To<TickServiceHandler>().AsSingle();
        }
    }
}
