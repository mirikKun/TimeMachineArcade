using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    public void UpdateTimer(float currentTime)
    {
        TimeSpan timeSpanTime = TimeSpan.FromSeconds(currentTime);
        _timerText.text = timeSpanTime.ToString("m\\.ss\\.f");
    }
}