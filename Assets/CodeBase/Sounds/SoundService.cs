using CodeBase.Infrastructure.AssetManagment;
using UnityEngine;
using UnityEngine.Audio;

namespace CodeBase.Sounds
{
    public class AudioService : IAudioService
    {
        private readonly AudioTypeSource _musicSource;
        private readonly AudioSource _simpleSfxSource;
        
        private AudioMixerGroup _sfxGroup;
        private AudioMixerGroup _musicGroup;


        public AudioService()
        {
            FindAudioMixer();
            
            _musicSource = CreateAudioSource("MusicSource", _musicGroup, 1f);
            _simpleSfxSource = CreateSimpleSfxAudioSource(_sfxGroup);
        }

        public void PlayMusic(AudioClip audioClip) => 
            _musicSource.StartCrossfade(audioClip);

        public void PlaySound(AudioClip audioClip, float volume = 1f) => 
            _simpleSfxSource.PlayOneShot(audioClip, volume);

        public void PlaySound(AudioClip audioClip, Vector3 at, float volume = 1f)
        {
            //create sound source at point and destroy 
            
            var source = CreateOneSound(audioClip, volume);
            source.transform.position = at;
        }

        public void PlaySound(AudioClip audioClip, Transform parent, float volume = 1f)
        {
            //create sound source like child of this transform 
            
            var source = CreateOneSound(audioClip, volume);
            source.transform.SetParent(parent);
            source.transform.localPosition = Vector3.zero;
        }

        private void FindAudioMixer()
        {
            var musicAudioMixer = Resources.Load<AudioMixer>(AssetPaths.AudioMixerPath);
            _musicGroup = musicAudioMixer.FindMatchingGroups("Master/Music")[0];
            _sfxGroup = musicAudioMixer.FindMatchingGroups("Master/SFX")[0];
        }

        private AudioTypeSource CreateAudioSource(string musicSourceName, AudioMixerGroup audioMixerGroup, float fadeTime)
        {
            var audioSource = new GameObject(musicSourceName).AddComponent<AudioTypeSource>();
            audioSource.Construct(audioMixerGroup, fadeTime);
            Object.DontDestroyOnLoad(audioSource.gameObject);
            return audioSource;
        }

        private AudioSource CreateSimpleSfxAudioSource(AudioMixerGroup audioMixerGroup)
        {
            var audioSource = new GameObject("Simple SFX Source").AddComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
            audioSource.spatialBlend = 0f;
            
            Object.DontDestroyOnLoad(audioSource.gameObject);
            return audioSource;
        }

        private AudioSource CreateOneSound(AudioClip audioClip, float volume)
        {
            var audioSource = new GameObject(audioClip.name).AddComponent<AudioSource>();
            
            audioSource.loop = false;
            audioSource.outputAudioMixerGroup = _sfxGroup;
            audioSource.spatialBlend = 1f;
            audioSource.volume = volume;
            audioSource.clip = audioClip;
            audioSource.Play();
            
            Object.Destroy(audioSource.gameObject, audioClip.length); //pull

            return audioSource;
        }
        
    }
}