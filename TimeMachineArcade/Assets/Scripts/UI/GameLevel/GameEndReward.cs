using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GameEndReward : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _rewardText;
        private IPersistentProgressService _progress;

        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService progress,
            ISaveLoadService saveLoadService)
        {
            _progress = progress;

            _saveLoadService = saveLoadService;
        }

        public void InitializeReward(int coins)
        {
            _progress.PlayerData.MoneyData.Collect(coins);
            UpdateReward(coins);
        }

        private void UpdateReward(int coins)
        {
            UpdateRewardText(coins);
            _saveLoadService.SaveProgress();
        }

        private void UpdateRewardText(int coins)
        {
            _rewardText.text = coins.ToString();
        }
    }
}