using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        private float _startPositionY;

        private float _endPositionY;

        private float _movingSpeedY;

        private float _positionX;

        private float _positionZ;

        private Transform _myTransform;

        [SerializeField]
        private Params _params;

        private void Awake()
        {
            this._startPositionY = this._params.StartPositionY;
            this._endPositionY = this._params.EndPositionY;
            this._movingSpeedY = this._params._movingSpeedY;
            this._myTransform = this.transform;
            var position = this._myTransform.position;
            this._positionX = position.x;
            this._positionZ = position.z;
        }

        private void FixedUpdate()
        {
            if (this._myTransform.position.y <= this._endPositionY)
            {
                this._myTransform.position = new Vector3(
                    this._positionX,
                    this._startPositionY,
                    this._positionZ
                );
            }

            this._myTransform.position -= new Vector3(
                this._positionX,
                this._movingSpeedY * Time.fixedDeltaTime,
                this._positionZ
            );
        }

        [Serializable]
        public sealed class Params
        {
            [SerializeField] private float _startPositionY;
            public float StartPositionY => _startPositionY;

            [SerializeField] private float _endPositionY;
            public float EndPositionY => _endPositionY;

            [SerializeField] public float _movingSpeedY;

            public float MovingSpeedY => _movingSpeedY;
        }
    }
}