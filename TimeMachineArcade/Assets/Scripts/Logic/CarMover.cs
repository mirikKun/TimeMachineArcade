using System;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Logic
{
    public class CarMover : MonoBehaviour
    {
        [SerializeField] private DriftingEffect _driftingEffect;
        [SerializeField] private AnimationCurve _dragOverDirection;
        [SerializeField] private AnimationCurve _wheelPowerOverSpeed;
        [SerializeField] private float _moveSpeed = 8;
        [SerializeField] private float _maxSpeed = 16;
        [SerializeField] private float _drag = 0.98f;
        [SerializeField] private float _steerAngle = 50;
        [SerializeField] private float _traction = 0.01f;
        [SerializeField] private float _angleToMinDrag = 60;
        [SerializeField] private float _angleToDriftProve = 40f;
        [SerializeField] private float _minSpeedToDrift = 5;

        [SerializeField] private float _xPositionLimit;
        [SerializeField] private float _rotationLimit;

        public bool _drifting;
        public event Action<int,Vector3,bool> OnObstacleHit;
        public event Action<float> OnMoving;
        public event Action OnPortalEnter;
        
        private float _currentDrag;

        private Vector3 _moveForce;
        private IInput _input;
        private Transform _transform;
        private Vector3 _startPosition;
        private float _forceAngle;


        [Inject]
        private void Construct(IInput input)
        {
            _input = input;
        }

        private void Start()
        {
            _transform = transform;
            _currentDrag = _drag;
            _startPosition = _transform.position;
        }

        private void FixedUpdate()
        {
       
            CarMoving();
            OnDriftingInvoking(_forceAngle);
        }

        public void Reset()
        {
            _moveForce = Vector3.zero;
            _transform.rotation = Quaternion.identity;
            MoveToStartPoint();
            _currentDrag = _drag;
        }

        public void MoveToStartPoint()
        {
            _transform.position = _startPosition;

        }
        public void OperateHit(float acceleration,int coins,Vector3 position)
        {
            Accelerate(acceleration);
            OnObstacleHit?.Invoke(coins,position,_drifting);
        }

        public void EnterPortal() => OnPortalEnter?.Invoke();

        private void Accelerate(float acceleration)
        {
            _moveForce *= acceleration;
            LimitMoveForce();
        }

        private void CarMoving()
        {
            _moveForce += GetForwardForce();
            _transform.position += _moveForce * Time.deltaTime;
            _transform.Rotate(GetRotation());
        
            LimitMoveForce();
            _moveForce = LerpForceToMoveDirection();

            LimitPosition();
            LimitRotation();
        
            _forceAngle = Vector3.Angle(_moveForce.normalized, _transform.forward);
            CalculateDrag(_forceAngle);
        }

        private void LimitPosition()
        {
            Vector3 position = _transform.position;
            float limitedX = Mathf.Clamp(position.x,-_xPositionLimit,_xPositionLimit);
            position = new Vector3(limitedX, position.y, position.z);
            _transform.position = position;
        }

        private void LimitRotation()
        {
            Vector3 rotation = _transform.eulerAngles;
            float yRotation = rotation.y;
            if (yRotation> 180)
            {
                yRotation -= 360;
            }
            float limitedYRotation = Mathf.Clamp(yRotation,-_rotationLimit,_rotationLimit);

            rotation = new Vector3(rotation.x, limitedYRotation, rotation.z);
            _transform.eulerAngles = rotation;
        }

        private void LimitMoveForce()
        {
            _moveForce *= _currentDrag;
            _moveForce = Vector3.ClampMagnitude(_moveForce, _maxSpeed);
        }

        private void CalculateDrag(float forceAngle)
        {
            float forceDiff = forceAngle / _angleToMinDrag;
            _currentDrag = _drag + (1 - _drag) * _dragOverDirection.Evaluate(forceDiff);
        }

        private Vector3 LerpForceToMoveDirection() =>
            Vector3.Lerp(_moveForce.normalized, _transform.forward, TractionDelta()) *
            _moveForce.magnitude;

        private void OnDriftingInvoking(float forceAngle)
        {
            if (forceAngle > _angleToDriftProve && _moveForce.magnitude > _minSpeedToDrift)
            {
                _drifting = true;
                _driftingEffect.EnableEffect();
            }
            else
            {
                _drifting = false;
            }
        }
        

        private float TractionDelta() => _traction * Time.deltaTime;


        private Vector3 GetRotation() => Vector3.up * (_input.RotateInput * _steerAngle * Time.deltaTime);

        private Vector3 GetForwardForce()
        {
            Vector3 forwardForce = transform.forward * (_moveSpeed * _input.GasInput * Time.deltaTime*_wheelPowerOverSpeed.Evaluate(_moveForce.magnitude/_maxSpeed));
            if (forwardForce.z > 0)
            {
                OnMoving?.Invoke(transform.position.z);
            }
            return forwardForce;
        }
    }
}