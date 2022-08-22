using System;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Sounds.SoundPlay
{
    public abstract class SoundPlayBase : MonoBehaviour
    {
        [SerializeField] protected AudioClip SoundClip;
        [SerializeField, Range(0,1f)] protected float Volume = 1;
        
        
        private IAudioPlayable [] _audioSourcers;
        protected IAudioService _audioService;

        private void Construct()
        {
            //_audioService = AllServices.Container.Single<IAudioService>();
        }

        private void Start()
        {
            Construct();
            
            _audioSourcers = GetComponents<IAudioPlayable>();

            Array.ForEach(_audioSourcers, z => z.AudioEvent += PlaySound);
        }

        private void OnDestroy()
        {
            Array.ForEach(_audioSourcers, z => z.AudioEvent -= PlaySound);
        }

        protected abstract void PlaySound();
    }
}