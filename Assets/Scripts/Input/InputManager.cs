using System;
using UnityEngine;

namespace ShootEmUp
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.OnShootInput?.Invoke();
            }
        }

        private void DirectionInputHandler()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.OnDirectionInput?.Invoke(DirectionTypeEnum.Left);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.OnDirectionInput?.Invoke(DirectionTypeEnum.Right);
            }
        }
    }
}