using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ColorBuyButton:MonoBehaviour
    {
        public Button Button;
        public ColorType Color;
        public TextMeshProUGUI PriceText;
        public int Price;

        private void Start()
        {
            PriceText.text = Price.ToString();
        }
    }
}