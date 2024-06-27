using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class PlayerPanelStatElement : MonoBehaviour
    {
        [SerializeField] private Image _previewImage;
        [SerializeField] private TextMeshProUGUI _titleText;
        
        private PlayerPanelStatElementData _initData;
        
        public void Init(PlayerPanelStatElementData initData)
        {
            _initData = initData;

            _previewImage.sprite = initData.PreviewSprite;
            _titleText.SetText(initData.Title);
        }
    }
}