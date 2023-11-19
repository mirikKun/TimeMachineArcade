using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class CustomCarData 
    {
        public ColorType CarColor;
        public AccessoriesType AccessoriesType;


        public List<ColorType> AvailableColors;
        public List<AccessoriesType> AvailableAccessories;
    }
}
