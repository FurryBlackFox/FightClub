using UnityEngine;

namespace Code.UI
{
    public class PlayerPanelStatElementData
    {
        public Sprite PreviewSprite { get; }
        public string Title { get; }

        public PlayerPanelStatElementData(Sprite previewSprite, string title)
        {
            PreviewSprite = previewSprite;
            Title = title;
        }

        public PlayerPanelStatElementData(IUiStatsData data)
        {
            PreviewSprite = data.PreviewSprite;
            Title = data.Title;
        }
    }
}