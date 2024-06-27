using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Services
{
    public class GameBuilder : MonoBehaviour
    {
        [SerializeField] private List<Player.Player> _players;

        private SettingsProvider _settingsProvider;
        
        private void Awake()
        {
            ServiceLocator.Instance.TryRegisterService(this);
            
            _settingsProvider = ServiceLocator.Instance.GetService<SettingsProvider>();
        }

        private void Start()
        {
            Rebuild();
        }

        public void Rebuild()
        {
            foreach (var player in _players)
            {
                var enemy = GetEnemyForPlayer(player);
                BuildPlayer(player, enemy, false);
            }
        }

        public void RebuildWithBuffs()
        {
            foreach (var player in _players)
            {
                var enemy = GetEnemyForPlayer(player);
                BuildPlayer(player, enemy, true);
            }
        }

        private Player.Player GetEnemyForPlayer(Player.Player player)
        {
            return _players.FirstOrDefault(p => p != player);
        }

        private void BuildPlayer(Player.Player player, Player.Player enemy, bool useBuffs)
        {
            var usedStatsList = _settingsProvider.StatLibrary;
            
            var usedBuffsList = new List<BuffLibraryData>();
            var buffsAmount = useBuffs
                ? Random.Range(_settingsProvider.GameSettings.buffCountMin,
                    _settingsProvider.GameSettings.buffCountMax)
                : 0;

            var availBuffs = new List<BuffLibraryData>(_settingsProvider.BuffLibrary);
            for (int i = 0; i < buffsAmount; i++)
            {
                var randomIndex = Random.Range(0, availBuffs.Count);
                if(randomIndex >= availBuffs.Count)
                    break;

                var buff = availBuffs[randomIndex];
                usedBuffsList.Add(buff);

                if (!_settingsProvider.GameSettings.allowDuplicateBuffs)
                    availBuffs.Remove(buff);
            }

            var runtimeStats = ParseStats(usedStatsList, usedBuffsList);

            var playerInitData = new PlayerInitData(usedStatsList, usedBuffsList, enemy, runtimeStats);
            player.Build(playerInitData);
        }
        
        private List<StatRuntimeData> ParseStats(List<StatLibraryData> usedStatsList, List<BuffLibraryData> usedBuffsList)
        {
            var resultStatsList = new List<StatRuntimeData>();
            
            foreach (var statLibraryData in usedStatsList)
            {
                var stat = new StatRuntimeData(statLibraryData.ID, statLibraryData.Value);
                resultStatsList.Add(stat);
            }

            foreach (var buffLibraryData in usedBuffsList)
            {
                if (buffLibraryData.StatModifiers == null)
                    continue;
                
                foreach (var statModifier in buffLibraryData.StatModifiers)
                {
                    var targetStat = resultStatsList.FirstOrDefault(s => s.ID == statModifier.statId);
                    if(targetStat == null)
                        continue;
                    
                    targetStat.Value += statModifier.value;
                }
            }

            return resultStatsList;
        }
        
    }
}