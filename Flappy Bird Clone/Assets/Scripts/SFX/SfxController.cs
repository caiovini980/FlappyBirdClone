using System.Collections.Generic;
using Base.Controllers;
using UnityEngine;
using Utils.TimeUtils;

namespace SFX
{
    public class SfxController : ControllerBase
    {
        [SerializeField] private AudioClip timerSound;
        [SerializeField] private List<AudioSource> sources;
        [Space(10)] 
        [SerializeField] private CountdownHandler countdownHandler;
        
        public void PlayAudio(AudioClip clip)
        {
            // find an available audio source
            AudioSource availableSource = GetAvailableAudioSource();

            if (availableSource == null) return;
            
            // play the audio on that audio source
            availableSource.clip = clip;
            availableSource.Play();
        }

        protected override void AwakeController()
        {
            SubscribeEvents();
        }

        protected override void EnableController()
        {
        }

        protected override void DisableController()
        {
            UnsubscribeEvents();
        }

        protected override void StartController()
        {
        }

        private AudioSource GetAvailableAudioSource()
        {
            foreach (AudioSource source in sources)
            {
                if (!source.isPlaying)
                {
                    return source;
                }
            }

            return null;
        }
        
        private void CountdownHandlerOnOnTimerUpdated(float seconds)
        {
            PlayAudio(timerSound); 
        }
        
        // EVENTS
        private void SubscribeEvents()
        {
            countdownHandler.OnTimerUpdated += CountdownHandlerOnOnTimerUpdated;
        }
        
        private void UnsubscribeEvents()
        {
            countdownHandler.OnTimerUpdated -= CountdownHandlerOnOnTimerUpdated;
        }
    }
}
