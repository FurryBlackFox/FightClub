using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Code.Data;
using Code.Utils;
using Newtonsoft.Json;
using UnityEngine;
using TextAsset = UnityEngine.TextAsset;

namespace Code.Services
{
    public class SettingsProvider
    {
        public CameraModel CameraSettings { get; private set; }
        public GameModel GameSettings { get; private set; }
        public List<StatLibraryData> StatLibrary { get; private set; } = new();
        public List<BuffLibraryData> BuffLibrary { get; private set; } = new();
        
        private ResourcesManager _resourcesManager;
        
        public SettingsProvider(ResourcesManager resourcesManager)
        {
            _resourcesManager = resourcesManager;
            
            
        }

        public async Task ReadSettingsFromFile()
        {
            var settingsFile = await _resourcesManager.LoadAsset<TextAsset>(Consts.Path.SettingsDataAssetPath);
            
            if(!settingsFile)
                return;

            var settingsData = JsonConvert.DeserializeObject<Data.Data>(settingsFile.text);
            
            if(settingsData == null)
                return;

            CameraSettings = settingsData.cameraSettings;
            GameSettings = settingsData.settings;

            foreach (var referenceStat in settingsData.stats)
            {
                var sprite = await _resourcesManager.LoadAsset<Sprite>(Path.Combine(Consts.Path.SpritesAssetPath, referenceStat.icon));
                var statData = new StatLibraryData(referenceStat.id, referenceStat.title, sprite, referenceStat.value);
                StatLibrary.Add(statData);
            }
            
            foreach (var referenceBuff in settingsData.buffs)
            {
                var sprite = await _resourcesManager.LoadAsset<Sprite>(Path.Combine(Consts.Path.SpritesAssetPath, referenceBuff.icon));
                var buffData = new BuffLibraryData(referenceBuff.id, referenceBuff.title, sprite, referenceBuff.stats);
                BuffLibrary.Add(buffData);
            }
        }
    }
}