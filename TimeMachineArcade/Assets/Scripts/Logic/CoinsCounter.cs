using System;
using ModestTree.Util;
namespace Logic
{
    public class  CoinsCounter
    {
        public int CurrentCoins { get; private set; }

        public event Action<int> OnChanged;

        public void ResetCoins()
        {
            CurrentCoins = 0;
            OnChanged?.Invoke(CurrentCoins);
        }

        public void AddCoins(int coins)
        {
            CurrentCoins += coins;
            OnChanged?.Invoke(CurrentCoins);
        }
    }
}