using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class AccessoryBuyButton : MonoBehaviour
    {
        public Button Button;
        public AccessoriesType Type;
        public TextMeshProUGUI PriceText;
        public int Price;

        private void Start()
        {
            PriceText.text = Price.ToString();
        }
    }
}