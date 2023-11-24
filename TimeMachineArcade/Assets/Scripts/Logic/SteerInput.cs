using Infrastructure.Services.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Logic
{
    public class SteerInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
    {
        [SerializeField] private RectTransform _wheelUITransform;
        [SerializeField] private Image _wheelUIImage;

        private bool _pressing;

        private IInput _input;
        private float _inputThreshold = 350;
        private float _minWheelSize = 0.3f;

        [Inject]
        private void Construct(IInput input)
        {
            _input = input;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _wheelUIImage.enabled = true;
            _pressing = true;
        }


        public void OnPointerMove(PointerEventData eventData)
        {
            if (!_pressing)
                return;

            Vector2 direction = CalculateInputDirection(eventData);
            float angle = AngleInput(direction);
            float speed = SpeedInput(direction);
            UiInputView(angle, speed);
        }

        private Vector2 CalculateInputDirection(PointerEventData eventData)
        {
            Vector2 touchPosition = eventData.position;
            Vector2 centerPosition = _wheelUITransform.position;
            Vector2 direction = touchPosition - centerPosition;
            return direction;
        }

        private float SpeedInput(Vector2 direction)
        {
            float speed = Vector2.ClampMagnitude(direction, _inputThreshold).magnitude / _inputThreshold;
            if (direction.y > 0)
            {
                speed = 0;
            }

            _input.UpdateSpeed(speed);
            return speed;
        }

        private float AngleInput(Vector2 direction)
        {
            float angle = Vector2.SignedAngle(Vector2.down, direction);
            
            angle = Mathf.Clamp(angle, -90, 90);
            float normAngle = angle / 90;
            _input.UpdateRotation(-normAngle);
            return angle;
        }

        private void UiInputView(float angle, float speed)
        {
            _wheelUITransform.eulerAngles = new Vector3(0, 0, angle);
            _wheelUITransform.localScale = Vector3.one * Mathf.Clamp(speed, _minWheelSize, 1);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _wheelUIImage.enabled = false;
            _pressing = false;
            _input.UpdateRotation(0);
            _input.UpdateSpeed(0);


        }
    }
}