using System;
using UnityEngine;

namespace Base.Controllers
{
    public abstract class ControllerBase : MonoBehaviour
    {
        protected abstract void AwakeController();
        protected abstract void EnableController();
        protected abstract void DisableController();
        protected abstract void StartController();
    
        private void Awake()
        {
            AwakeController();
        }

        private void OnEnable()
        {
            EnableController();
        }
    
        private void OnDisable()
        {
            DisableController();
        }

        private void Start()
        {
            StartController();
        }
    }
}
