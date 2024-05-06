using System.Collections.Generic;
using ShootEmUp.Common.Dispatcher;
using ShootEmUp.GameCycle.Models;
using UnityEngine;

namespace ShootEmUp.GameCycle
{
    public class GameTickManager : MonoBehaviour, IGameStartListener, IGameEndListener, IGamePauseListener,
        IGameResumeListener
    {
        private readonly List<IGameFixedTickableListener> _fixTickedList = new();
        private readonly List<IGameTickableListener> _tickedList = new();
        private bool _isActiveState = true;

        private void Awake()
        {
            this._isActiveState = false;

            IGameTickListener.OnRegister +=
                listener =>
                {
                    DispatcherSingletone.RunOnMainThread(() =>
                    {
                        switch (listener)
                        {
                            case IGameFixedTickableListener { } fixedTickableListener:
                                _fixTickedList.Add(fixedTickableListener);
                                break;
                            case IGameTickableListener tickableListener:
                                _tickedList.Add(tickableListener);
                                break;
                        }
                    });
                };
        }

        private void Start()
        {
            IGameEventListener.Register(this);
        }

        private void Update()
        {
            if (!this._isActiveState)
            {
                return;
            }

            foreach (var element in this._tickedList)
            {
                element.Tick();
            }
        }

        private void FixedUpdate()
        {
            if (!this._isActiveState)
            {
                return;
            }

            foreach (var element in this._fixTickedList)
            {
                element.FixedTick();
            }
        }

        public void OnGameStart()
        {
            this._isActiveState = true;
        }

        public void OnGameEnd()
        {
            this._isActiveState = false;
        }

        public void OnGamePause()
        {
            this._isActiveState = false;
        }

        public void OnGameResume()
        {
            this._isActiveState = true;
        }
    }
}