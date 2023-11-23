using System;
using ModestTree.Util;
namespace Logic
{
    public class  CoinsCounter
    {
        public int ActualCurrentCoins { get; private set; }
        public int CurrentCoinsForVisual { get; private set; }

        public event Action<int> OnChanged;

        public void ResetCoins()
        {
            ActualCurrentCoins = 0;
            CurrentCoinsForVisual = 0;
            OnChanged?.Invoke(ActualCurrentCoins);
        }

        public void AddVisualCoins(int coins)
        {
            CurrentCoinsForVisual += coins;
            OnChanged?.Invoke(CurrentCoinsForVisual);
        }
        public void AddActualCoins(int coins)
        {
            ActualCurrentCoins += coins;
        }
    }
}