using Car_Jam_Pavlov.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Car_Jam_Pavlov.Infrastructure.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private PathService _pathService;

        public override void InstallBindings()
        {
            Container.BindInstance(_pathService).AsSingle().NonLazy();
        }
    }
}
