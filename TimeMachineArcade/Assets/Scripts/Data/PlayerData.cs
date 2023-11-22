using System;

namespace Data
{
    [Serializable]

    public class PlayerData
    {
        public CustomCarData CustomCarData;
        public PointsData PointsData;
        public MoneyData MoneyData;

        public PlayerData()
        {
            CustomCarData = new();
            PointsData = new();
            MoneyData = new();
        }
    }
}