using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class MoneyData
    {
        public int _coins;
        public Action Changed;

        public void Collect(int newCoins)
        {
            _coins += newCoins;
            Changed?.Invoke();
        }

        public bool CanBuy(int price)
        {
            return price < _coins;
        }
        public void Spend(int coins)
        {
            if(!CanBuy(coins))
                return;
            _coins -= coins;
            Changed?.Invoke();
        }
    }
}