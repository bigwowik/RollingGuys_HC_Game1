
using CodeBase.Infrastructure.Services;
using CodeBase.Logic.Map;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(Vector3 at);
        GameObject CreateHud();
        void CleanUp();
        void CreateDialogUI();
        void CreateEnemySpawner(Vector3 at);
        GameObject PlayerGameObject { get; set; }
        IMapCreator CreateMapCreator();
        GameObject CreatePlayerCamera();
    }
}