using System;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure
{
    public interface ISceneLoader : IService
    {
        void Load(string nextScene, Action onLoad = null);
    }
}