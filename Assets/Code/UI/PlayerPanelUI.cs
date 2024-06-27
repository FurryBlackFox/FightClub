using System;
using System.Collections.Generic;
using System.Linq;
using Code.Player;
using Code.UI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelUI : MonoBehaviour
{
    [SerializeField] private PlayerPanelStatElement _statElementPrefab;
    
    [SerializeField] private Button _attackButton;
    [SerializeField] private Transform _statsPanelRoot;
    [SerializeField] private Player _player;

    private List<PlayerPanelStatElement> _spawnedPlayerStatElements = new();
    
    private void Awake()
    {
        _attackButton.onClick.AddListener(AttackEnemy);
        
        _player.OnPlayerInitialized += OnPlayerInitialized;
    }

    private void OnDestroy()
    {
        _attackButton.onClick.RemoveListener(AttackEnemy);
        
        _player.OnPlayerInitialized -= OnPlayerInitialized;
    }
    
    private void AttackEnemy()
    {
        _player.AttackEnemy();
    }

    private void OnPlayerInitialized()
    {
        //TODO: pool
        foreach (var spawnedPlayerStatElement in _spawnedPlayerStatElements)
        {
            Destroy(spawnedPlayerStatElement.gameObject);
        }
        _spawnedPlayerStatElements.Clear();

        foreach (var statLibraryData in _player.InitData.UsedStatsList)
        {
            var element = Instantiate(_statElementPrefab, _statsPanelRoot);
            var statValue = _player.InitData.StatRuntimeDataList.FirstOrDefault(s => s.ID == statLibraryData.ID)?.Value ?? 0;
            element.Init(new PlayerPanelStatElementData(statLibraryData.PreviewSprite, statValue.ToString()));
            _spawnedPlayerStatElements.Add(element);
        }
        
        foreach (var statData in _player.InitData.UsedBuffsList)
        {
            var element = Instantiate(_statElementPrefab, _statsPanelRoot);
            element.Init(new PlayerPanelStatElementData(statData));
            _spawnedPlayerStatElements.Add(element);
        }
    }
}
