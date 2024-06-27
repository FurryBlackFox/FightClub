using System;
using UnityEngine;

namespace Code.UI
{
    public class PlayerCanvas : MonoBehaviour
    {
        private Camera _mainCamera;
        
        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            transform.forward = _mainCamera.transform.forward;
        }
    }
}