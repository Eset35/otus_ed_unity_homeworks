using System;
using ShootEmUp.Common.Input;
using UnityEngine;

namespace ShootEmUp.Input
{
    public sealed class InputManager : MonoBehaviour
    {
        public Action OnShootInput;
        public Action<DirectionTypeEnum> OnDirectionInput;
        
        private void Update()
        {
            this.ShootInputHandler();
            this.DirectionInputHandler();
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
    }
}