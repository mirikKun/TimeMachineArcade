using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
   public class Popup : MonoBehaviour
   {
      [SerializeField] private GameObject _popupObject;
      [SerializeField] private TextMeshProUGUI _popupText;
      [SerializeField] private Button _button;

      private void Start()
      {
         _button.onClick.AddListener(ClosePopup);
      }

      public void OpenPopup(string message)
      {
         _popupText.text = message;
         _popupObject.SetActive(true);
      }

      private void ClosePopup()
      {
         _popupObject.SetActive(false);
      }
   }
}
