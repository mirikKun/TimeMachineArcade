using Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using Zenject;

public class CoinsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _number;
    private IPersistentProgressService _progressService;

    [Inject]
    private void Construct(IPersistentProgressService progressService)
    {
        _progressService = progressService;
    }

    private void Start()
    {
        UpdateText();
        _progressService.PlayerData.MoneyData.Changed += UpdateText;
    }

    private void OnDestroy()
    {
        if (_progressService.PlayerData != null)
            _progressService.PlayerData.MoneyData.Changed -= UpdateText;
    }

    private void UpdateText()
    {
        _number.text = _progressService.PlayerData.MoneyData._coins.ToString();
    }
}