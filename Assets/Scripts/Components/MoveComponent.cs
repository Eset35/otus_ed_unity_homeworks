using ShootEmUp.Common.Input;
using UnityEngine;

namespace ShootEmUp.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            this._rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(DirectionTypeEnum directionTypeEnum, float speed = 5.0f)
        {
            if (directionTypeEnum == DirectionTypeEnum.Left)
            {
                Move(new Vector2(-1, 0) * Time.fixedDeltaTime);
            }
            else
            {
                Move(new Vector2(1, 0) * Time.fixedDeltaTime);
            }
        }
    
        public void Move(Vector2 vector, float speed = 5.0f)
        {
            var nextPosition = this._rigidbody2D.position + vector * speed;
            this._rigidbody2D.MovePosition(nextPosition);
        }
    }
}