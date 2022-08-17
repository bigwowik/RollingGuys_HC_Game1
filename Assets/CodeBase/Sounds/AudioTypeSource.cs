using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace CodeBase.Sounds
{
    public class AudioTypeSource : MonoBehaviour
    {
        private float _timeToFade = 1f;

        private AudioSource _audioSource1;
        private AudioSource _audioSource2;
        private GameObject _audioSourceGameObject;


        public void Construct(AudioMixerGroup audioMixerGroup, float fadeTime)
        {
            _timeToFade = fadeTime;
            
            _audioSource1 = gameObject.AddComponent<AudioSource>();
            _audioSource2 = gameObject.AddComponent<AudioSource>();
            
            _audioSource1.outputAudioMixerGroup = audioMixerGroup;
            _audioSource2.outputAudioMixerGroup = audioMixerGroup;
        }
        public void StartCrossfade(AudioClip newTrack, bool loop = true)
        {
            _audioSource1.loop = loop;
            _audioSource2.loop = loop;
            Debug.Log("<color=grey> [SoundManager] </color> Start Music");
            StopAllCoroutines();
            StartCoroutine(Crossfade(newTrack, _audioSource1, _audioSource2));
        }

        

        private IEnumerator Crossfade(AudioClip newTrack, AudioSource source1, AudioSource source2)
        {
            float timeElapsed = 0f;

            if (source1.isPlaying)
            {
                source2.clip = newTrack;
                source2.Play();

                while (timeElapsed < _timeToFade)
                {
                    source2.volume = Mathf.Lerp(0, 1, timeElapsed / _timeToFade);
                    source1.volume = Mathf.Lerp(1, 0, timeElapsed / _timeToFade);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }

                source1.Stop();
                source1.clip = null;
            }
            else
            {
                source1.clip = newTrack;
                source1.Play();

                while (timeElapsed < _timeToFade)
                {
                    source1.volume = Mathf.Lerp(0, 1, timeElapsed / _timeToFade);
                    source2.volume = Mathf.Lerp(1, 0, timeElapsed / _timeToFade);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }

                source2.Stop();
                source2.clip = null;
            }
        }

        private IEnumerator Fade(AudioSource source, bool fadeIn)
        {
            float timeElapsed = 0f;
            float currentVolume = fadeIn ? 0 : 1;
            float targetVolume = fadeIn ? 1 : 0;

            while (timeElapsed < _timeToFade)
            {
                source.volume = Mathf.Lerp(currentVolume, targetVolume, timeElapsed / _timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            source.Stop();
            source.clip = null;
            source.volume = 1;
        }
    }
}