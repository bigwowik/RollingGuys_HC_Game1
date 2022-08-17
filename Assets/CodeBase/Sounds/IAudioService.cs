using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Sounds
{
    public interface IAudioService : IService
    {
        void PlayMusic(AudioClip audioClip);
        void PlaySound(AudioClip audioClip, float volume);
        void PlaySound(AudioClip audioClip, Vector3 at, float volume);
        void PlaySound(AudioClip audioClip, Transform parent, float volume);
    }
}