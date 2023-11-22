using System;
using TMPro;
using UnityEngine;

namespace UI.GameLevel
{
    public class UiTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        public void UpdateTimer(float currentTime)
        {
            TimeSpan timeSpanTime = TimeSpan.FromSeconds(currentTime);
            _timerText.text = timeSpanTime.ToString("m\\.ss\\.f");
        }
    }
}