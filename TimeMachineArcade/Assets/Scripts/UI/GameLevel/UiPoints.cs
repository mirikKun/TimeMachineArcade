using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiPoints : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pointsText;

    public void UpdatePointsText(float currentPoints)
    {
        _pointsText.text = ((int)currentPoints).ToString();
    }
}