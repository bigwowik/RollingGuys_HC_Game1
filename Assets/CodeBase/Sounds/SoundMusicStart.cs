using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Sounds
{
    public class SoundMusicStart : MonoBehaviour
    {
        public AudioClip Music;
        private IAudioService _soundService;

        public void Construct()
        {
            _soundService = AllServices.Container.Single<IAudioService>();
        }

        void Start()
        {
            Construct();

            _soundService.PlayMusic(Music);
        }
    }
}