using Assets._Scripts.Manager.__Base;
using Assets._Scripts.Manager.Sound.Enum;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets._Scripts.Manager.Sound.Enum.SoundManagerEnum;

namespace Assets._Scripts.Manager.Sound
{
    public class SoundManager : BaseManager<SoundManager>
    {
        [SerializeField]
        private AudioSource sourceSFX;
        [SerializeField]
        private AudioSource sourceMusic;

        private readonly List<AudioClip> audioClips = new();

        private float volumeSFX;
        public float VolumeSFX { get { return volumeSFX; } }

        private float volumeMusic;
        public float VolumeMusic { get { return volumeMusic; } }

        private string sfxProperty;
        private string musicProperty;

        private Coroutine playMusicCR;

        protected override void OnInitialize()
        {
            SetProperties();
        }

        private void SetProperties()
        {
            sfxProperty = $"{Application.productName}_VolumeSFX";
            musicProperty = $"{Application.productName}_VolumeMusic";

            volumeSFX = PlayerPrefs.GetFloat(sfxProperty, 1);
            volumeMusic = PlayerPrefs.GetFloat(musicProperty, 1);

            sourceSFX.volume = volumeSFX;
            sourceMusic.volume = volumeMusic;

            if (global::System.Enum.IsDefined(typeof(SoundManagerEnum.Music), 0))
                PlayMusic(0);
        }

        private IEnumerator PlayMusicCR(SoundManagerEnum.Music music)
        {
            var file = $"{(int)music}_{(music.ToString().Replace($"_{(int)music}", ""))}";

            if (sourceMusic.isPlaying && sourceMusic.clip.name == file)
            {
                sourceMusic.DOKill();
                sourceMusic.DOFade(volumeMusic, 1)
                    .SetUpdate(true);

                yield break;
            }

            bool complete = false;

            sourceMusic.DOKill();
            sourceMusic.DOFade(0, 1)
                .OnComplete(() => complete = true)
                .SetUpdate(true);

            yield return new WaitUntil(() => complete);

            sourceMusic.Stop();

            if (sourceMusic.clip != null)
                Resources.UnloadAsset(sourceMusic.clip);

            complete = true;

            ResourceRequest request = Resources.LoadAsync<AudioClip>($"Manager/Sound/Music/{file}");

            yield return request;

            sourceMusic.clip = (AudioClip)request.asset;

            if (sourceMusic.clip == null)
                yield break;

            sourceMusic.clip.name = file;
            sourceMusic.Play();
            sourceMusic.DOFade(volumeMusic, 1);
        }

        public void PlaySFX(SoundManagerEnum.SFX sfx)
        {
            try
            {
                var file = $"{(int)sfx}_{(sfx.ToString().Replace($"_{(int)sfx}", ""))}";

                AudioClip clip = audioClips.Find(x => x.name == file);

                if (clip == default)
                {
                    clip = Resources.Load<AudioClip>($"Manager/Sound/SFX/{file}");

                    if (clip != default)
                    {
                        clip.name = sfx.ToString();

                        audioClips.Add(clip);
                    }
                }

                if (sourceSFX != null && clip != default)
                    sourceSFX.PlayOneShot(clip);
            }
            catch(Exception ex) 
            {
                Debug.LogError($"{sfx} : {ex.Message}");
            }
        }

        public void PlayMusic(SoundManagerEnum.Music music)
        {
            try
            {
                if (playMusicCR != null)
                    StopCoroutine(playMusicCR);

                playMusicCR = StartCoroutine(PlayMusicCR(music));
            }
            catch (Exception ex)
            {
                Debug.LogError($"{music} : {ex.Message}");
            }
        }

        public void StopSFX()
        {
            sourceSFX.Stop();
        }

        public void StopMusic()
        {
            sourceMusic.DOKill();
            sourceMusic.DOFade(0, 1)
                .OnComplete(() => 
                {
                    sourceMusic.Stop();

                    if (sourceMusic.clip != null)
                        Resources.UnloadAsset(sourceMusic.clip);
                })
                .SetUpdate(true);
        }

        public void PauseMusic()
        {
            sourceMusic.Pause();
        }

        public void ResumeMusic()
        {
            sourceMusic.UnPause();
        }

        public void SetVolumeSFX(float volume)
        {
            sourceSFX.volume = volume;

            volumeSFX = volume;

            PlayerPrefs.SetFloat(sfxProperty, volumeSFX);
            PlayerPrefs.Save();
        }

        public void SetVolumeMusic(float volume)
        {
            sourceMusic.volume = volume;

            volumeMusic = volume;

            PlayerPrefs.SetFloat(musicProperty, volumeMusic);
            PlayerPrefs.Save();
        }
    }
}
