using TMPro;
using UnityEngine;

namespace UI.GameLevel
{
    public class UiPoints : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _pointsText;

        public void UpdatePointsText(float currentPoints)
        {
            _pointsText.text = ((int)currentPoints).ToString();
        }
    }
}