using System;
using Data.IapData;

namespace Data
{
    [Serializable]

    public class PlayerData
    {
        public CustomCarData CustomCarData;
        public PointsData PointsData;
        public MoneyData MoneyData;

        public PurchaseData PurchaseData;
        public PlayerData()
        {
            CustomCarData = new();
            PointsData = new();
            MoneyData = new();
        }
    }
}