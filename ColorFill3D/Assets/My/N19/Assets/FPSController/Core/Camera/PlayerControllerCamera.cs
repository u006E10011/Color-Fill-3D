using UnityEngine;

using static N19.InputPlayer;

namespace N19
{
    public class PlayerControllerCamera
    {
        public PlayerControllerCamera(Transform owner, Transform camera, Transform model, PlayerControllerData data)
        {
            _camera = camera;
            _owner = owner;
            _model = model;
            _data = data;

            OnRotation = data.CameraType == CameraType.FPS ? FPS : TPS_2;
        }

        public readonly System.Action OnRotation;

        private float _rotationX;
        private float _rotationY;
        private float _sensitivity;

        private readonly PlayerControllerData _data;
        private readonly Transform _camera;
        private readonly Transform _owner;
        private readonly Transform _model;

        public void SetSensivity(float value) => _sensitivity = value;

        public void FPS()
        {
            _rotationX -= Instance.MouseDirection.y * _sensitivity * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, _data.RotateMinMax.Min, _data.RotateMinMax.Max);

            _camera.localRotation = Quaternion.Euler(_rotationX, _camera.localRotation.y, _camera.localRotation.z);
            _owner.Rotate((_sensitivity * Instance.MouseDirection.x * Vector3.up) * Time.deltaTime);
        }

        public void TPS_2()
        {
            _rotationX -= Instance.MouseDirection.y * _sensitivity * Time.deltaTime;
            _rotationY += Instance.MouseDirection.x * _sensitivity * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, _data.RotateMinMax.Min, _data.RotateMinMax.Max);

            _camera.localRotation = Quaternion.Euler(_rotationX, _rotationY, _camera.localRotation.z);
        }

        public void RotateToDirection()
        {
            if (Instance.MoveDirection.x != 0 || Instance.MoveDirection.y != 0)
            {
                _model.rotation = Quaternion.Slerp(_model.rotation, _camera.rotation, _data.SpeedRotationModel * Time.deltaTime);
                _model.rotation = Quaternion.Euler(0, _model.eulerAngles.y, 0);
            }
        }

    }
}