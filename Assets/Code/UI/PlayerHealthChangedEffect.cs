using System;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class PlayerHealthChangedEffect : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI _healhText;
        [SerializeField] public CanvasGroup _canvasGroup;
        
        [SerializeField] public float _moveDistance = 1f;
        [SerializeField] public float _lifetimeDuration = 1f;

        private float _timer;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Init(string sign, float value)
        {
            _healhText.SetText($"{sign}{value:f0}");
        }

        private void LateUpdate()
        {
            var deltaMove = _moveDistance * Time.deltaTime / _lifetimeDuration;
            var movement = Vector2.up * deltaMove;
            _rectTransform.anchoredPosition += movement;

            _canvasGroup.alpha = 1 - _timer / _lifetimeDuration;

            _timer += Time.deltaTime;
            if(_timer >= _lifetimeDuration)
                Destroy(gameObject);
        }
    }
}