using System;
using Code.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class GameBuilderUI : MonoBehaviour
    {
        [SerializeField] private Button _rebuildButton;

        [SerializeField] private Button _rebuildWithBuffsButton;

        private GameBuilder _gameBuilder;

        private void Awake()
        {
            _rebuildButton.onClick.AddListener(OnRebuildButtonClick);
            _rebuildWithBuffsButton.onClick.AddListener(OnRebuildWithBuffsButtonClick);
        }

        private void Start()
        {
            _gameBuilder = ServiceLocator.Instance.GetService<GameBuilder>();
        }

        private void OnDestroy()
        {
            _rebuildButton.onClick.RemoveListener(OnRebuildButtonClick);
            _rebuildWithBuffsButton.onClick.RemoveListener(OnRebuildWithBuffsButtonClick);
        }

        private void OnRebuildButtonClick()
        {
            _gameBuilder.Rebuild();
        }

        private void OnRebuildWithBuffsButtonClick()
        {
            _gameBuilder.RebuildWithBuffs();
        }

}
}