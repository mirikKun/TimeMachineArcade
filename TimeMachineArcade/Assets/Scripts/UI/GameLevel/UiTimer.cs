using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameLevel
{
    public class UiTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        [SerializeField] private Image _imageTimer;
        private float _maxTime=1;

        public void InitTimer(float maxTime)
        {
            _maxTime = maxTime;
        }
        public void UpdateTimer(float currentTime)
        {
            UpdateText(currentTime);
            UpdateImage(currentTime);
        }

        private void UpdateImage(float currentTime)
        {
            _imageTimer.fillAmount= (_maxTime-currentTime)/_maxTime;
        }

        private void UpdateText(float currentTime)
        {
            TimeSpan timeSpanTime = TimeSpan.FromSeconds(currentTime);
            _timerText.text = timeSpanTime.ToString("m\\.ss\\.f");
        }
    }
}