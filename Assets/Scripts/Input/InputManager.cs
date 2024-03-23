using System;
using UnityEngine;

namespace ShootEmUp
{
    public abstract class AbstractInputListener : MonoBehaviour
    {
        public Action OnShootInput;
        public Action<DirectionTypeEnum> OnDirectionInput;

        protected abstract void ShootInputHandler();
        protected abstract void DirectionInputHandler();
    }
    
    public sealed class InputManager : AbstractInputListener
    {
        private void Update()
        {
            this.ShootInputHandler();
            this.DirectionInputHandler();
        }

        protected override void ShootInputHandler()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.OnShootInput?.Invoke();
            }
        }

        protected override void DirectionInputHandler()
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