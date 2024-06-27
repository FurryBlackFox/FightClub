using System;
using Code.Data;
using Code.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code
{
    public class CameraAnimator : MonoBehaviour
    {
        [SerializeField] private Transform _cameraRoot;
        [SerializeField] private Camera _camera;

        private CameraModel _cameraSettings;
        private SettingsProvider _settingsProvider;


        private float _roamingTimer;
        private Vector3 _cameraStartPosition;
        private Vector3 _roamingStartPosition;
        private Vector3 _roamingTargetPosition;

        private bool _fovStage;
        private float _fovTimer;
        
        private void Start()
        {
            _settingsProvider = ServiceLocator.Instance.GetService<SettingsProvider>();
            _cameraSettings = _settingsProvider.CameraSettings;

            _camera.fieldOfView = _cameraSettings.fovMax;
            
            SetInitialCameraPos();

            _roamingTimer = _cameraSettings.roamingDuration;
            _cameraStartPosition = _camera.transform.localPosition;
            _roamingStartPosition = _cameraStartPosition;
            _roamingTargetPosition = _cameraStartPosition;
        }

        private void SetInitialCameraPos()
        {
            var cameraPos = _camera.transform.position;
            var rootPos = _cameraRoot.transform.position;
            
            var vectorToCamera = cameraPos - rootPos;
            vectorToCamera.y = 0;
            var directionToCamera = vectorToCamera.normalized;
            cameraPos = rootPos + directionToCamera * _cameraSettings.roundRadius;
            
            var rootHeight = rootPos.y;
            var cameraHeight = rootHeight + _cameraSettings.height;
            cameraPos.y = cameraHeight;
            
            _camera.transform.position = cameraPos;
        }

        private void LateUpdate()
        {
            RoundEffect();
            
            RoamingEffect();
            
            FovEffect();
            
            LookAtEffect();
        }

        private void RoundEffect()
        {
            var rotationPerSecond = 360 * Time.deltaTime / _cameraSettings.roundDuration;
            var currentRotation = _cameraRoot.localRotation.eulerAngles;
            _cameraRoot.localRotation = Quaternion.AngleAxis(currentRotation.y + rotationPerSecond, Vector3.up);
        }

        private void LookAtEffect()
        {
            var targetPos = _cameraRoot.transform.position;
            targetPos.y = _cameraSettings.lookAtHeight;
            
            var vectorToRoot = targetPos - _camera.transform.position;
            _camera.transform.forward = vectorToRoot;
        }

        private void RoamingEffect()
        {
            _roamingTimer += Time.deltaTime;
            
            var roamingProgress = _roamingTimer / _cameraSettings.roamingDuration;
            var currentPosition = Vector3.Lerp(_roamingStartPosition, _roamingTargetPosition, roamingProgress);
            _camera.transform.localPosition = currentPosition;

            if (_roamingTimer > _cameraSettings.roamingDuration)
            {
                var roamingOffset = Random.insideUnitSphere * _cameraSettings.roamingRadius;
                _roamingStartPosition = _roamingTargetPosition;
                _roamingTargetPosition = _cameraStartPosition + roamingOffset;
                _roamingTimer = 0;
            }
        }
        
        private void FovEffect()
        {
            _fovTimer += Time.deltaTime;
            
            if(_fovTimer < _cameraSettings.fovDelay)
                return;

            var startFov = _fovStage
                ? _cameraSettings.fovMin
                : _cameraSettings.fovMax;
            
            var endFov = _fovStage
                ? _cameraSettings.fovMax
                : _cameraSettings.fovMin;

            var fovChangeProgress = (_fovTimer - _cameraSettings.fovDelay) / _cameraSettings.fovDuration;
            _camera.fieldOfView = Mathf.Lerp(startFov, endFov, fovChangeProgress);

            if(_fovTimer < _cameraSettings.fovDelay + _cameraSettings.fovDuration)
                return;

            _fovStage = !_fovStage;
            _fovTimer = 0;
        }
    }
}