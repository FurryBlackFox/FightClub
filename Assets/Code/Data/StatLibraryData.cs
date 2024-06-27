using Code.UI;
using UnityEngine;

namespace Code.Data
{
    public class StatLibraryData : IUiStatsData
    {
        public int ID { get; }
        public string Title { get; }
        public Sprite PreviewSprite { get; }
        public float Value { get; }

        public StatLibraryData(int id, string title, Sprite previewSprite, float value)
        {
            ID = id;
            Title = title;
            PreviewSprite = previewSprite;
            Value = value;
        }

        public StatLibraryData(StatLibraryData reference)
        {
            ID = reference.ID;
            Title = reference.Title;
            PreviewSprite = reference.PreviewSprite;
            Value = reference.Value;
        }
    }
}