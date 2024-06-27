using Code.UI;
using UnityEngine;

namespace Code.Data
{
    public class BuffLibraryData : IUiStatsData
    {
        public int ID { get; }
        public string Title { get; }
        public Sprite PreviewSprite { get; }
        public BuffStat[] StatModifiers { get; }
        
        public BuffLibraryData(int id, string title, Sprite previewSprite, BuffStat[] statModifiers)
        {
            ID = id;
            Title = title;
            PreviewSprite = previewSprite;
            StatModifiers = statModifiers;
        }
    }
}