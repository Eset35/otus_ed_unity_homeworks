using System;
using ShootEmUp.Common.Input;
using ShootEmUp.GameCycle.Models;
using UnityEngine;

namespace ShootEmUp.Input
{
    public sealed class InputManager : MonoBehaviour, IGameTickableListener
    {
        public Action OnShootInput;
        public Action<DirectionTypeEnum> OnDirectionInput;

        private void Start()
        {
            IGameTickableListener.Register(this);
        }

        private void ShootInputHandler()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                this.OnShootInput?.Invoke();
            }
        }

        private void DirectionInputHandler()
        {
            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
            {
                this.OnDirectionInput?.Invoke(DirectionTypeEnum.Left);
            }
            else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
            {
                this.OnDirectionInput?.Invoke(DirectionTypeEnum.Right);
            }
        }

        public void Tick()
        {
            this.ShootInputHandler();
            this.DirectionInputHandler();
        }
    }
}