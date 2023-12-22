using System.Collections;
using UnityEngine;

namespace Utils.TimeUtils
{
    public class CountdownHandler : MonoBehaviour
    {
        // DELEGATES
        public delegate void TimerUpdated(float seconds);
        public delegate void TimerFinished();
        
        // EVENTS
        public event TimerUpdated OnTimerUpdated;
        public event TimerFinished OnTimerFinished;
        
        // VARIABLES
        private Coroutine _startTimerCoroutine;

        // METHODS
        public void StartCountdown(float duration)
        {
            _startTimerCoroutine = StartCoroutine(Countdown(duration));
        }
         
        // TODO Change coroutine to async
        private IEnumerator Countdown(float duration)
        {
            for (float i = duration; i > 0; i--)
            {
                yield return new WaitForSecondsRealtime(1);
                OnTimerUpdated?.Invoke(i);
            }
            
            OnTimerFinished?.Invoke();
        }
    }
}
