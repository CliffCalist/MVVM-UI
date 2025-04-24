using System;
using R3;
using UnityEngine;

namespace WhiteArrow.ReactiveUI
{
    public abstract class ViewAnimations : IDisposable
    {
        [SerializeField] private bool _isEnabled = true;


        private bool _isInitialized;



        public bool IsEnabled
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }


        protected readonly Subject<Unit> _showEnded = new();
        public Observable<Unit> ShowEnded => _showEnded;

        protected readonly Subject<Unit> _hideEnded = new();
        public Observable<Unit> HideEnded => _hideEnded;



        internal void Init(UIView view)
        {
            if (_isInitialized)
                throw new Exception($"The {GetType().Name} is already initialized.");

            InitCore(view);
            _isInitialized = true;
        }

        protected abstract void InitCore(UIView view);



        private void ThrowIfNonInitialized()
        {
            if (!_isInitialized)
                throw new Exception($"The {GetType().Name} isn't initialized.");
        }



        internal void PlayShow()
        {
            ThrowIfNonInitialized();
            PlayShowCore();
        }

        protected abstract void PlayShowCore();



        internal void PlayHide()
        {
            ThrowIfNonInitialized();
            PlayHideCore();
        }

        protected abstract void PlayHideCore();



        public virtual void Dispose()
        {
            _showEnded.Dispose();
            _hideEnded.Dispose();
        }
    }
}