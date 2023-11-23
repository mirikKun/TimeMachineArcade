using TMPro;
using UnityEngine;

namespace UI.GameLevel
{
    public class UiCoins : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsText;

        public void UpdateCoinsText(int currentCoins)
        {
            _coinsText.text = (currentCoins).ToString();
        }
    }
}